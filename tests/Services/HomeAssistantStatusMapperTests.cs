using HaTeamsWatcher.Models;
using HaTeamsWatcher.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace HaTeamsWatcher.Tests.Services
{
    public class HomeAssistantStatusMapperTests
    {
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly HomeAssistantStatusMapper _statusMapper;

        public HomeAssistantStatusMapperTests()
        {
            _mockConfig = new Mock<IConfiguration>();
            _statusMapper = new HomeAssistantStatusMapper(_mockConfig.Object);
        }

        [Theory]
        [InlineData(TeamsStatus.Available, Constants.Configuration.StatusMappings.Statuses.Available, "Available", "mdi:account")]
        [InlineData(TeamsStatus.Away, Constants.Configuration.StatusMappings.Statuses.Away, "Away", "mdi:account-arrow-left-outline")]
        [InlineData(TeamsStatus.BRB, Constants.Configuration.StatusMappings.Statuses.BRB, "Be right back", "mdi:account-arrow-left-outline")]
        [InlineData(TeamsStatus.Busy, Constants.Configuration.StatusMappings.Statuses.Busy, "Busy", "mdi:account-cancel")]
        [InlineData(TeamsStatus.DoNotDisturb, Constants.Configuration.StatusMappings.Statuses.DoNotDisturb, "Do not disturb", "mdi:account-cancel")]
        [InlineData(TeamsStatus.Focusing, Constants.Configuration.StatusMappings.Statuses.Focusing, "Focusing", "mdi:magnify")]
        [InlineData(TeamsStatus.InAMeeting, Constants.Configuration.StatusMappings.Statuses.InAMeeting, "In a meeting", "mdi:account-group")]
        [InlineData(TeamsStatus.Offline, Constants.Configuration.StatusMappings.Statuses.Offline, "Offline", "mdi:account-outline")]
        [InlineData(TeamsStatus.OnACall, Constants.Configuration.StatusMappings.Statuses.OnACall, "On a call", "mdi:headset")]
        [InlineData(TeamsStatus.Presenting, Constants.Configuration.StatusMappings.Statuses.Presenting, "Presenting", "mdi:presentation")]
        [InlineData(TeamsStatus.Unknown, Constants.Configuration.StatusMappings.Statuses.Unknown, "Unknown", "mdi:account-question-outline")]
        public void MapFromTeamsStatus_GivenTeamsStatus_ReturnsNameAndIconFromConfig(TeamsStatus teamsStatus, string configKey, string name, string icon)
        {
            // Arrange
            var nameConfigSection = new Mock<IConfigurationSection>();
            var iconConfigSection = new Mock<IConfigurationSection>();

            _mockConfig.Setup(x => x.GetSection($"{Constants.Configuration.StatusMappings.SectionName}:{configKey}:{Constants.Configuration.StatusMappings.StatusValues.Name}"))
                .Returns(nameConfigSection.Object)
                .Verifiable();

            _mockConfig.Setup(x => x.GetSection($"{Constants.Configuration.StatusMappings.SectionName}:{configKey}:{Constants.Configuration.StatusMappings.StatusValues.Icon}"))
                .Returns(iconConfigSection.Object)
                .Verifiable();

            nameConfigSection.Setup(x => x.Value)
                .Returns(name)
                .Verifiable();

            iconConfigSection.Setup(x => x.Value)
                .Returns(icon)
                .Verifiable();

            // Act
            var status = _statusMapper.MapFromTeamsStatus(teamsStatus);

            // Assert
            Assert.Equal(name, status.Name);
            Assert.Equal(icon, status.Icon);
            _mockConfig.Verify();
            nameConfigSection.Verify();
            iconConfigSection.Verify();
        }
    }
}