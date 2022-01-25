using HaTeamsWatcher.Interfaces;
using HaTeamsWatcher.Models;
using HaTeamsWatcher.Services;
using Moq;
using System;
using Xunit;

namespace HaTeamsWatcher.Tests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IConsole> _mockConsole;
        private readonly AuthenticationService _service;

        public AuthenticationServiceTests()
        {
            _mockConsole = new Mock<IConsole>();
            _service = new AuthenticationService(_mockConsole.Object);
        }

        [Fact]
        public void GetAuthenticationHeaderValue_GivenBasicAuthenticationScheme_ReturnsBasicAuthHeaderFromCredentials()
        {
            // Arrange
            const string user = "user";
            const string password = "password";
            const string expectedEncodedCredentials = "dXNlcjpwYXNzd29yZA==";

            _mockConsole.Setup(x => x.ReadLine())
                .Returns(user)
                .Verifiable();

            _mockConsole.SetupSequence(x => x.ReadKey(It.IsAny<bool>()))
                .Returns(new ConsoleKeyInfo(password[0], ConsoleKey.P, false, false, false))
                .Returns(new ConsoleKeyInfo(password[1], ConsoleKey.A, false, false, false))
                .Returns(new ConsoleKeyInfo(password[2], ConsoleKey.S, false, false, false))
                .Returns(new ConsoleKeyInfo(password[3], ConsoleKey.S, false, false, false))
                .Returns(new ConsoleKeyInfo(password[4], ConsoleKey.W, false, false, false))
                .Returns(new ConsoleKeyInfo(password[5], ConsoleKey.O, false, false, false))
                .Returns(new ConsoleKeyInfo(password[6], ConsoleKey.R, false, false, false))
                .Returns(new ConsoleKeyInfo(password[7], ConsoleKey.D, false, false, false))
                .Returns(new ConsoleKeyInfo(Convert.ToChar(10), ConsoleKey.Enter, false, false, false));

            // Act
            var authHeaderValue = _service.GetAuthenticationHeaderValue(Constants.Authentication.Schemes.Basic);

            // Assert
            Assert.Equal(expectedEncodedCredentials, authHeaderValue.Parameter);
            Assert.Equal(Constants.Authentication.Schemes.Basic, authHeaderValue.Scheme);
            _mockConsole.Verify();
            _mockConsole.Verify(x => x.ReadKey(It.IsAny<bool>()), Times.Exactly(9));
        }

        [Fact]
        public void GetAuthenticationHeaderValue_GivenNoneAuthenticationScheme_ReturnsNull()
        {
            // Arrange / Act
            var authHeaderValue = _service.GetAuthenticationHeaderValue(Constants.Authentication.Schemes.None);

            // Assert
            Assert.Null(authHeaderValue);
        }

        [Theory]
        [InlineData("Digest")]
        [InlineData("IntegratedWindowsAuthentication")]
        [InlineData("Negotiate")]
        [InlineData("Ntlm")]
        public void GetAuthenticationHeaderValue_GivenOtherAuthenticationScheme_ThrowsNotSupportedException(string scheme)
        {
            // Arrange / Act / Assert
            Assert.Throws<NotSupportedException>(() => _service.GetAuthenticationHeaderValue(scheme));
        }
    }
}