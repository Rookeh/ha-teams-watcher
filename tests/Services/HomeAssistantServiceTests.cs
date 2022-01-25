using HaTeamsWatcher.Interfaces;
using HaTeamsWatcher.Models;
using HaTeamsWatcher.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using Xunit;

namespace HaTeamsWatcher.Tests.Services
{
    public class HomeAssistantServiceTests
    {
        private readonly Mock<IAuthenticationService> _mockAuthService;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly Mock<IHttpClient> _mockHttpClient;
        private readonly Mock<ILogger<HomeAssistantService>> _mockLogger;

        private readonly HomeAssistantService _service;

        public HomeAssistantServiceTests()
        {
            _mockAuthService = new Mock<IAuthenticationService>();
            _mockConfig = new Mock<IConfiguration>();
            _mockHttpClient = new Mock<IHttpClient>();
            _mockLogger = new Mock<ILogger<HomeAssistantService>>();

            var authConfigSection = new Mock<IConfigurationSection>();
            authConfigSection.Setup(x => x.Value)
                .Returns(Constants.Authentication.Schemes.None);

            _mockConfig.Setup(x => x.GetSection($"{Constants.Configuration.HomeAssistant.SectionName}:{Constants.Configuration.HomeAssistant.Authentication.SectionName}:{Constants.Configuration.HomeAssistant.Authentication.Scheme}"))
                .Returns(authConfigSection.Object);

            _mockHttpClient.Setup(x => x.DefaultRequestHeaders)
                .Returns(new HttpClient().DefaultRequestHeaders);

            _service = new HomeAssistantService(_mockAuthService.Object, _mockConfig.Object,
                _mockHttpClient.Object, _mockLogger.Object);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void Update_GivenStatus_PostStatusToHomeAssistantInstance(bool success)
        {
            // Arrange
            var status = new HomeAssistantStatus { Name = "Test", Icon = "mdi:test-tube" };
            var response = success ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.InternalServerError);
            var webHookUrl = "http://localhost:8192";

            var webHookConfigSection = new Mock<IConfigurationSection>();
            webHookConfigSection.Setup(x => x.Value)
                .Returns(webHookUrl)
                .Verifiable();

            _mockConfig.Setup(x => x.GetSection($"{Constants.Configuration.HomeAssistant.SectionName}:{Constants.Configuration.HomeAssistant.WebHookUrl}"))
                .Returns(webHookConfigSection.Object)
                .Verifiable();

            _mockHttpClient.Setup(x => x.SendAsync(It.Is<HttpRequestMessage>(r => GetRequestMatches(r, status, webHookUrl))))
                .ReturnsAsync(response)
                .Verifiable();

            // Act
            var result = await _service.Update(status);

            // Assert
            Assert.Equal(success, result);
            webHookConfigSection.Verify();
            _mockConfig.Verify();
            _mockHttpClient.Verify();
        }

        #region Private methods

        private static bool GetRequestMatches(HttpRequestMessage r, HomeAssistantStatus status, string webHookUrl)
        {
            var json = JsonConvert.DeserializeObject<HomeAssistantStatus>(r.Content.ReadAsStringAsync().Result);
            return r.RequestUri.OriginalString == webHookUrl && json.Name == status.Name && json.Icon == status.Icon;
        }

        #endregion
    }
}