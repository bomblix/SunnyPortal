﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Bomblix.SunnyPortal.Core
{
    public class SunnyPortal
    {
        private readonly string password;
        private readonly string username;

        private CookieContainer container;

        public bool IsConnected
        {
            get; private set;
        }

        public SunnyPortal( string username, string password )
        {
            this.password = password;
            this.username = username;
        }

        public ConnectionResult Connect()
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
                reguestParameters.Add( Constants.LoginButtonParameter, "Login" );
                reguestParameters.Add( Constants.ServiceAccessParameter, true.ToString() );
                reguestParameters.Add( Constants.EventTargetParamerter, string.Empty );
                reguestParameters.Add( Constants.EventArgumentParameter, string.Empty );
                reguestParameters.Add( Constants.ViewStateParameter, string.Empty );
                reguestParameters.Add( Constants.ViewStateGeneratorParameter, string.Empty );
                reguestParameters.Add( Constants.HiddenLanguageParameter, Constants.PortalCulture );


                byte[] responseBytes = wc.UploadValues( Constants.LoginUrl, Constants.RequestMethod, reguestParameters );

                string responseBody = Encoding.UTF8.GetString( responseBytes );

                this.container = wc.CookieContainer;

                this.IsConnected = responseBody.Contains( Constants.UserNameLabelName );

                return new ConnectionResult
                {
                    Message = IsConnected ? string.Empty : "Invalid login or password",
                };
            }
        }

        public int GetCurrentPower()
        {
            if ( !IsConnected )
            {
                throw new Exception( "You are not logged in SunnyPortal." );
            }

            using ( var z = new CookieAwareWebClient( container ) )
            {
                string jsonResult = z.DownloadString( string.Format( Constants.LiveDataUrl, DateTime.Now.Millisecond ) );
                var liveData = Newtonsoft.Json.JsonConvert.DeserializeObject<LiveData>( jsonResult );
                return liveData.PV;
            }
        }

        public Dictionary<string, float> GetHistoricalData( DateTime date )
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
                requestParameters.Add( Constants.DateSelectionIntervalId, "3" );
                requestParameters.Add( Constants.DateSelectionDateTextBox, date.ToString( Constants.DateFormat ) );

                webClient.UploadValues( Constants.SelectDateUrl, Constants.RequestMethod, requestParameters );

                var csvContent = webClient.DownloadString( Constants.DownloadUrl );

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
