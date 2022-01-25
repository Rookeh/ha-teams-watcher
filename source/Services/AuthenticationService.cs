using System;
using System.Net.Http.Headers;
using System.Text;
using HaTeamsWatcher.Interfaces;
using HaTeamsWatcher.Models;

namespace HaTeamsWatcher.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConsole _console;

        public AuthenticationService(IConsole console)
        {
            _console = console;
        }

        public AuthenticationHeaderValue GetAuthenticationHeaderValue(string scheme)
        {
            switch (scheme)
            {
                case Constants.Authentication.Schemes.Basic:
                    return BuildBasicAuthHeaderValue();
                case Constants.Authentication.Schemes.None:
                    return null;
                default:
                    throw new NotSupportedException($"Unsupported authentication scheme: {scheme}");
            }
        }

        private AuthenticationHeaderValue BuildBasicAuthHeaderValue()
        {
            _console.WriteLine("Basic Auth - Username:");
            var username = _console.ReadLine();
            _console.WriteLine("Basic Auth - Password:");
            string password = string.Empty;
            while (true)
            {
                var key = _console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                password += key.KeyChar;
            }

            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

            return new AuthenticationHeaderValue(Constants.Authentication.Schemes.Basic, encoded);
        }
    }
}