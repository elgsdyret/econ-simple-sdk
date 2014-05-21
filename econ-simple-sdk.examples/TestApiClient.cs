using System;
using System.Configuration;

namespace econ_simple_sdk.examples
{
    public class TestApiClient
    {
        public static EconomicWebService Connect()
        {
            var appToken = ConfigurationManager.AppSettings.Get("appToken");
            if(string.IsNullOrEmpty(appToken))
                throw new Exception("appToken missing");

            var grantToken = ConfigurationManager.AppSettings.Get("grantToken");
            if (string.IsNullOrEmpty(grantToken))
                throw new Exception("grantToken missing");

            var url = ConfigurationManager.AppSettings.Get("url");

            return string.IsNullOrEmpty(url) ? ApiClient.Connect(grantToken, appToken) : ApiClient.Connect(grantToken, appToken, url);
        }
    }
}