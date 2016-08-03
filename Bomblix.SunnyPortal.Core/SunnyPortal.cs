using System;
using System.Net;
using System.Text;

namespace Bomblix.SunnyPortal.Core
{
    public class SunnyPortal
    {
        private const string UserNameParameter = "ctl00$ContentPlaceHolder1$Logincontrol1$txtUserName";
        private const string PasswordParameter = "ctl00$ContentPlaceHolder1$Logincontrol1$txtPassword";
        private const string LoginButtonParameter = "ctl00$ContentPlaceHolder1$Logincontrol1$LoginBtn";
        private const string ServiceAccessParameter = "ctl00$ContentPlaceHolder1$Logincontrol1$ServiceAccess";
        private const string EventTargetParamerter = "__EVENTTARGET";
        private const string EventArgumentParameter = "__EVENTARGUMENT";
        private const string ViewStateParameter = "__VIEWSTATE";
        private const string ViewStateGeneratorParameter = "__VIEWSTATEGENERATOR";
        private const string HiddenLanguageParameter = "ctl00$ContentPlaceHolder1$hiddenLanguage";
        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.82 Safari/537.36 OPR/39.0.2256.43";
        private const string RequestMethod = "POST";

        private const string PortalUrl = "https://www.sunnyportal.com/";
        private const string LoginUrl = "Templates/Start.aspx?logout=true";
        private const string UserNameLabelName = "ctl00_Header_lblUserName";

        private readonly string password;
        private readonly string username;
        private CookieContainer container;

        public bool IsConnected
        {
            get; set;
        }

        public SunnyPortal(string username, string password)
        {
            this.password = password;
            this.username = username;
        }

        public ConnectionResult Connect()
        {

            using ( CookieAwareWebClient wc = new CookieAwareWebClient() )
            {
                wc.Headers.Add( HttpRequestHeader.UserAgent, UserAgent );

                var reguestParameters = new System.Collections.Specialized.NameValueCollection();

                reguestParameters.Add( UserNameParameter, username );
                reguestParameters.Add( PasswordParameter, password );
                reguestParameters.Add( LoginButtonParameter, "Login" );
                reguestParameters.Add( ServiceAccessParameter, true.ToString() );
                reguestParameters.Add( EventTargetParamerter, string.Empty );
                reguestParameters.Add( EventArgumentParameter, string.Empty );
                reguestParameters.Add( ViewStateParameter, string.Empty );
                reguestParameters.Add( ViewStateGeneratorParameter, string.Empty );
                reguestParameters.Add( HiddenLanguageParameter, "en-us" );


                byte[] responseBytes = wc.UploadValues( string.Concat( PortalUrl, "/", LoginUrl ), RequestMethod, reguestParameters );
                string responseBody = Encoding.UTF8.GetString( responseBytes );

                this.container = wc.CookieContainer;

                this.IsConnected = responseBody.Contains( UserNameLabelName );

                return new ConnectionResult
                {
                    Message = IsConnected ? string.Empty : "Invalid login or password",
                };
            }
        }

        public int GetCurrentPower()
        {
            if ( !this.IsConnected )
            {
                return -1;
            }

            using ( var z = new CookieAwareWebClient( container ) )
            {
                string jsonResult = z.DownloadString( string.Format( "https://www.sunnyportal.com/homemanager?t={0}", DateTime.Now.Millisecond ) );
                var liveData = Newtonsoft.Json.JsonConvert.DeserializeObject<LiveData>( jsonResult );
                return liveData.PV;
            }
        }
    }

    internal class CookieAwareWebClient : WebClient
    {
        public CookieAwareWebClient()
            : this( new CookieContainer() )
        { }
        public CookieAwareWebClient( CookieContainer c )
        {
            this.CookieContainer = c;
        }
        public CookieContainer CookieContainer { get; set; }

        protected override WebRequest GetWebRequest( Uri address )
        {
            WebRequest request = base.GetWebRequest( address );

            var castRequest = request as HttpWebRequest;
            if ( castRequest != null )
            {
                castRequest.CookieContainer = this.CookieContainer;
            }

            return request;
        }
    }
}
