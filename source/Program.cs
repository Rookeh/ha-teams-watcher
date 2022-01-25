using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HaTeamsWatcher.Interfaces;
using HaTeamsWatcher.Services;
using HaTeamsWatcher.Wrappers;

namespace HaTeamsWatcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IHttpClient, HttpClientWrapper>();
                    services.AddSingleton<IConsole, ConsoleWrapper>();
                    services.AddSingleton<IAuthenticationService, AuthenticationService>();
                    services.AddSingleton<IHomeAssistantService, HomeAssistantService>();
                    services.AddSingleton<IHomeAssistantStatusMapper, HomeAssistantStatusMapper>();
                    services.AddSingleton<IFile, FileWrapper>();
                    services.AddSingleton<ITeamsStatusService, TeamsStatusService>();
                    services.AddHostedService<Worker>();
                });
    }
}