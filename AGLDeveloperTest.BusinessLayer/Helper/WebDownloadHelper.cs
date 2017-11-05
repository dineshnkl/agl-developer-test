using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AGLDeveloperTest.BusinessLayer.Helper
{
    public static class WebDownloadHelper
    {
        public static async Task<HttpResponseMessage> Get(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var responseMessage = await httpClient.GetAsync(url);
                    return responseMessage;
                }
            }
            catch (InvalidOperationException ivEx)
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                responseMessage.RequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                responseMessage.Content = new StringContent(ivEx.Message);
                responseMessage.RequestMessage.Content = responseMessage.Content;
                return responseMessage;
            }
        }

        public static async Task<string> ReadString(HttpResponseMessage responseMessage)
        {
            string responseText = null;
            if (responseMessage.Content != null)
                responseText = await responseMessage.Content.ReadAsStringAsync();
            return responseText;
        }
    }
}
