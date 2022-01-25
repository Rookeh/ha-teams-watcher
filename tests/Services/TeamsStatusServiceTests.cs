using HaTeamsWatcher.Interfaces;
using HaTeamsWatcher.Models;
using HaTeamsWatcher.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IO;
using System.Text;
using Xunit;

namespace HaTeamsWatcher.Tests.Services
{
    public class TeamsStatusServiceTests
    {
        private readonly Mock<IConfigurationSection> _mockConfigSection;
        private readonly Mock<IFile> _mockFile;

        private readonly TeamsStatusService _service;

        public TeamsStatusServiceTests()
        {
            _mockConfigSection = new Mock<IConfigurationSection>();
            _mockFile = new Mock<IFile>();

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("Teams:LogFile"))
                .Returns(_mockConfigSection.Object);

            _service = new TeamsStatusService(mockConfig.Object, _mockFile.Object);
        }

        [Theory]
        [InlineData(Constants.Teams.StatusIndicatorStateService.Available, TeamsStatus.Available)]
        [InlineData(Constants.Teams.StatusIndicatorStateService.Away, TeamsStatus.Away)]
        [InlineData(Constants.Teams.StatusIndicatorStateService.Brb, TeamsStatus.BRB)]
        [InlineData(Constants.Teams.StatusIndicatorStateService.Busy, TeamsStatus.Busy)]
        [InlineData(Constants.Teams.StatusIndicatorStateService.Dnd, TeamsStatus.DoNotDisturb)]
        [InlineData(Constants.Teams.StatusIndicatorStateService.Focusing, TeamsStatus.Focusing)]
        [InlineData(Constants.Teams.StatusIndicatorStateService.OnThePhone, TeamsStatus.OnACall)]
        [InlineData(Constants.Teams.StatusIndicatorStateService.InAMeeting, TeamsStatus.InAMeeting)]
        [InlineData(Constants.Teams.StatusIndicatorStateService.Presenting, TeamsStatus.Presenting)]
        [InlineData(Constants.Teams.StatusIndicatorStateService.Offline, TeamsStatus.Offline)]
        [InlineData("", TeamsStatus.Unknown)]
        [InlineData(null, TeamsStatus.Unknown)]
        public void GetCurrentStatus_GivenStatusIndicatorStateServiceFromFile_ReturnsStatusAsEnum(string fileContent, TeamsStatus expectedStatus)
        {
            // Arrange
            const string filePath = "logs.txt";
            var fileStream = new MemoryStream(Encoding.UTF8.GetBytes($"{Constants.Teams.StatusIndicatorStateService.Prefix} {fileContent}"));

            _mockConfigSection.Setup(x => x.Value)
                .Returns(filePath)
                .Verifiable();

            _mockFile.Setup(x => x.Open(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                .Returns(fileStream)
                .Verifiable();

            // Act
            var status = _service.GetCurrentStatus();

            // Assert
            Assert.Equal(expectedStatus, status);
            _mockConfigSection.Verify();
            _mockFile.Verify();
        }

        [Theory]
        [InlineData(Constants.Teams.OverlayIcon.AwayIcon, TeamsStatus.Away)]
        [InlineData(Constants.Teams.OverlayIcon.BrbIcon, TeamsStatus.BRB)]
        [InlineData(Constants.Teams.OverlayIcon.BusyIcon, TeamsStatus.Busy)]
        [InlineData(Constants.Teams.OverlayIcon.DndIcon, TeamsStatus.DoNotDisturb)]
        [InlineData(Constants.Teams.OverlayIcon.FocusingIcon, TeamsStatus.Focusing)]
        [InlineData(Constants.Teams.OverlayIcon.InACallIcon, TeamsStatus.OnACall)]
        [InlineData(Constants.Teams.OverlayIcon.InAMeeting, TeamsStatus.InAMeeting)]
        [InlineData(Constants.Teams.OverlayIcon.OfflineIcon, TeamsStatus.Offline)]
        [InlineData(Constants.Teams.OverlayIcon.OnThePhoneIcon, TeamsStatus.OnACall)]
        [InlineData(Constants.Teams.OverlayIcon.PresentingIcon, TeamsStatus.Presenting)]
        public void GetCurrentStatus_GivenOverlayIconStateFromFile_ReturnsStatusAsEnum(string fileContent, TeamsStatus expectedStatus)
        {
            // Arrange
            const string filePath = "logs.txt";
            var fileStream = new MemoryStream(Encoding.UTF8.GetBytes($"{Constants.Teams.OverlayIcon.Prefix} {fileContent}"));

            _mockConfigSection.Setup(x => x.Value)
                .Returns(filePath)
                .Verifiable();

            _mockFile.Setup(x => x.Open(It.IsAny<string>(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                .Returns(fileStream)
                .Verifiable();

            // Act
            var status = _service.GetCurrentStatus();

            // Assert
            Assert.Equal(expectedStatus, status);
            _mockConfigSection.Verify();
            _mockFile.Verify();
        }
    }
}
