using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HaTeamsWatcher.Interfaces;
using HaTeamsWatcher.Models;

namespace HaTeamsWatcher.Services
{
    public class HomeAssistantService : IHomeAssistantService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClient _httpClient;
        private readonly ILogger<HomeAssistantService> _logger;

        public HomeAssistantService(IAuthenticationService authService, IConfiguration config, IHttpClient httpClient, ILogger<HomeAssistantService> logger)
        {
            _config = config;
            _httpClient = httpClient;
            _logger = logger;
            var authScheme = _config.GetValue<string>($"{Constants.Configuration.HomeAssistant.SectionName}:{Constants.Configuration.HomeAssistant.Authentication.SectionName}:{Constants.Configuration.HomeAssistant.Authentication.Scheme}");
            _httpClient.DefaultRequestHeaders.Authorization = authService.GetAuthenticationHeaderValue(authScheme);
        }

        public async Task<bool> Update(HomeAssistantStatus status)
        {
            var webHookUrl = _config.GetValue<string>($"{Constants.Configuration.HomeAssistant.SectionName}:{Constants.Configuration.HomeAssistant.WebHookUrl}");
            var webHookRequest = new HttpRequestMessage(HttpMethod.Post, webHookUrl);
            webHookRequest.Content = JsonContent.Create(status);

            var response = await _httpClient.SendAsync(webHookRequest);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                _logger.LogError($"Failed to update Home Assistant. Response: {response.StatusCode}");
                return false;
            }
        }
    }
}