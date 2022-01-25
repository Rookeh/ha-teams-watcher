using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HaTeamsWatcher.Interfaces;

namespace HaTeamsWatcher.Wrappers
{
    public class HttpClientWrapper : IHttpClient
    {
        private static readonly HttpClient _instance = new HttpClient();

        public HttpRequestHeaders DefaultRequestHeaders
        {
            get
            {
                return _instance.DefaultRequestHeaders;
            }
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return _instance.SendAsync(request);
        }
    }
}