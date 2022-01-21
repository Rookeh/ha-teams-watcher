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
            var authScheme = _config.GetSection(Constants.Configuration.HomeAssistant.SectionName)
                .GetSection(Constants.Configuration.HomeAssistant.Authentication.SectionName)
                .GetValue<string>(Constants.Configuration.HomeAssistant.Authentication.Scheme);
            _httpClient.DefaultRequestHeaders.Authorization = authService.GetAuthenticationHeaderValue(authScheme);
        }

        public async Task<bool> Update(HomeAssistantStatus status)
        {
            var webhookUrl = _config.GetSection(Constants.Configuration.HomeAssistant.SectionName).GetValue<string>(Constants.Configuration.HomeAssistant.WebHookUrl);
            var webhookRequest = new HttpRequestMessage(HttpMethod.Post, webhookUrl);
            webhookRequest.Content = JsonContent.Create(status);

            var response = await _httpClient.SendAsync(webhookRequest);

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