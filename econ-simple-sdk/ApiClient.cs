using System.Net;

namespace econ_simple_sdk
{
    public class ApiClient
    {
        public static EconomicWebService Connect(string userGrantId, string privateAppId)
        {
            return Connect(userGrantId, privateAppId, "https://api.e-conomic.com/secure/api1/economicwebservice.asmx");
        }

        public static EconomicWebService Connect(string userGrantId, string privateAppId, string url)
        {
            var client = new EconomicWebService
            {
                Url = url,
                CookieContainer = new CookieContainer()
            };
            client.ConnectWithToken(userGrantId, privateAppId);
            return client;
        }
    }
}
