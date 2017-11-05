using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AGLDeveloperTest.BusinessLayer.Helper
{
    public static class WebDownloadHelper
    {
        public static HttpResponseMessage Get(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var responseMessage = httpClient.GetAsync(url).Result;
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
            //No internet connection
            catch (AggregateException aggEx)
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                var message = string.Join(Environment.NewLine, aggEx.InnerExceptions.Select(ex =>
                                           string.Concat(ex.Message, Environment.NewLine,
                                           ex.InnerException == null ? string.Empty : ex.InnerException.Message)));
                message = string.Concat(aggEx.Message, Environment.NewLine, message);
                responseMessage.RequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                responseMessage.Content = new StringContent(message);
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
