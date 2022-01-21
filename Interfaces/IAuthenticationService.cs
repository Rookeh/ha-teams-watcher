using System.Net.Http.Headers;

namespace HaTeamsWatcher.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationHeaderValue GetAuthenticationHeaderValue(string scheme);
    }
}