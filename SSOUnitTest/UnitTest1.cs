using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SSOUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private HttpClient _httpClient;

        public UnitTest1()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://mysso.local.cn/");
        }

        /// <summary>
        /// form验证
        /// </summary>
        [TestMethod]
        public void Get_Accesss_Token_By_Client_Credentials_Grant()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("client_id", "1234");
            parameters.Add("client_secret", "5678");
            parameters.Add("grant_type", "client_credentials");

            Console.WriteLine(_httpClient.PostAsync("/Token", new FormUrlEncodedContent(parameters))
                .Result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// base验证
        /// </summary>
       [TestMethod]
        public void Get_Accesss_Token_By_Client_Credentials_Grant_Basic()
        {
            var clientId = "1234";
            var clientSecret = "5678";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret)));

            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "client_credentials");

           var result = _httpClient.PostAsync("/Token", new FormUrlEncodedContent(parameters))
               .Result.Content.ReadAsStringAsync().Result;


           Console.WriteLine(result);
        }

        [TestMethod]
        public void TestAuth()
        {
            var result = _httpClient.GetAsync(@"api/values").Result.Content.ReadAsStringAsync().Result;
            result = result;
        }

        private string GetAccessToken()
        {
            return @"PaVR2IItrCgozCta2cVyPuvODG8ep9Ch1go6ru_vlMC3F4YDCRlSyB1Di7wTh8zM4CR85o2kfJJ-xV8I3IGWAEq3PjaDNPfRoOWEZbU3VFrHDTp2didk12Z3ePoLRFIuMjuV6kSmLZ0FrIq2DYjeh4DnJwFbQa5USQFjSx59OqEt6r_FNGlvN6qyhZqy6s9QSE3sUOesUc6oulHrjLVxHA";
        }

        [TestMethod]
        public void Call_WebAPI_By_Access_Token()
        {
            var token = GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = _httpClient.GetAsync(@"api/values").Result.Content.ReadAsStringAsync().Result;
            result = result;
        }

    }
}
