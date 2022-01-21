using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HaTeamsWatcher.Interfaces
{
    public interface IHttpClient
    {
        HttpRequestHeaders DefaultRequestHeaders { get; }
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}