using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bomblix.SunnyPortal.Core
{
    public class SunnyPortal
    {
        private CookieContainer container;

        public bool IsConnected
        {
            get; private set;
        }


        public async Task<ConnectionResult> Connect( string username, string password )
        {
            if ( string.IsNullOrEmpty( username ) && string.IsNullOrEmpty( password ) )
            {
                throw new Exception( "Your login and password cannot be empty" );
            }

            using ( CookieAwareWebClient wc = new CookieAwareWebClient() )
            {
                wc.Headers.Add( HttpRequestHeader.UserAgent, Constants.UserAgent );

                var reguestParameters = new System.Collections.Specialized.NameValueCollection();

                reguestParameters.Add( Constants.UserNameParameter, username );
                reguestParameters.Add( Constants.PasswordParameter, password );
                reguestParameters.Add( Constants.LoginButtonParameter, Constants.LoginButtonValue );
                reguestParameters.Add( Constants.ServiceAccessParameter, true.ToString() );
                reguestParameters.Add( Constants.EventTargetParamerter, string.Empty );
                reguestParameters.Add( Constants.EventArgumentParameter, string.Empty );
                reguestParameters.Add( Constants.ViewStateParameter, string.Empty );
                reguestParameters.Add( Constants.ViewStateGeneratorParameter, string.Empty );
                reguestParameters.Add( Constants.HiddenLanguageParameter, Constants.PortalCulture );

                byte[] responseBytes = await wc.UploadValuesTaskAsync( Constants.LoginUrl, Constants.RequestMethod,reguestParameters);

                string responseBody = Encoding.UTF8.GetString( responseBytes );

                this.container = wc.CookieContainer;

                this.IsConnected = responseBody.Contains( Constants.UserNameLabelName );

                return new ConnectionResult
                {
                    Message = IsConnected ? string.Empty : "Invalid login or password",
                };
            }
        }

        public async Task<int> GetCurrentPower()
        {
            if ( !IsConnected )
            {
                throw new Exception( "You are not logged in SunnyPortal." );
            }

            using ( var z = new CookieAwareWebClient( container ) )
            {
                string jsonResult = await z.DownloadStringTaskAsync( new Uri( string.Format( Constants.LiveDataUrl, DateTime.Now.Millisecond ) ) );
                try
                {
                    var liveData = Newtonsoft.Json.JsonConvert.DeserializeObject<LiveData>( jsonResult );

                    return liveData.PV;
                }
                catch ( JsonSerializationException )
                {
                    return 0;
                }
            }
        }

        public async Task<Dictionary<string, float>> GetHistoricalDailyData( DateTime date )
        {
            if ( !IsConnected )
            {
                throw new Exception( "You are not logged in SunnyPortal." );
            }

            using ( var webClient = new CookieAwareWebClient( container ) )
            {
                var requestParameters = new System.Collections.Specialized.NameValueCollection();

                // Open inverter selection - without this cannot set the download date;
                webClient.DownloadString( Constants.SelectDateUrl );

                requestParameters.Add( Constants.EventTargetParamerter, Constants.DateSelectionDatePicker );
                requestParameters.Add( Constants.DateSelectionIntervalId, "2" );
                requestParameters.Add( Constants.DateSelectionDateTextBox, date.ToString( Constants.DateFormat ) );
                webClient.UploadValues( Constants.SelectDateUrl, Constants.RequestMethod, requestParameters );

                var csvContent = await webClient.DownloadStringTaskAsync( Constants.DownloadUrl );

                return CsvHelper.ExtractToDictionary( csvContent );
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
