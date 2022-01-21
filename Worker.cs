using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using HaTeamsWatcher.Interfaces;
using HaTeamsWatcher.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace HaTeamsWatcher
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _config;
        private readonly IHomeAssistantService _homeAssistantService;
        private readonly IHomeAssistantStatusMapper _statusMapper;
        private readonly ITeamsStatusService _teamsStatusService;
        private readonly ILogger<Worker> _logger;

        private TeamsStatus? _currentStatus;

        public Worker(IConfiguration config, IHomeAssistantService homeAssistantService, IHomeAssistantStatusMapper statusMapper,
            ITeamsStatusService teamsStatusService, ILogger<Worker> logger)
        {
            _config = config;
            _homeAssistantService = homeAssistantService;
            _statusMapper = statusMapper;
            _teamsStatusService = teamsStatusService;
            _logger = logger;
            _currentStatus = null;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var newStatus = _teamsStatusService.GetCurrentStatus();

                if (!_currentStatus.HasValue || _currentStatus.Value != newStatus)
                {
                    var status = _statusMapper.MapFromTeamsStatus(_teamsStatusService.GetCurrentStatus());
                    _logger.LogInformation($"Sending new status ({status.Name}) to Home Assistant...");

                    var success = await _homeAssistantService.Update(status);
                    if (success)
                    {
                        _logger.LogInformation("Status updated.");
                        _currentStatus = newStatus;
                    }
                }

                var intervalSeconds = _config.GetSection(Constants.Configuration.Teams.SectionName)
                    .GetValue<int>(Constants.Configuration.Teams.Interval);

                await Task.Delay(TimeSpan.FromSeconds(intervalSeconds), stoppingToken);
            }
        }
    }
}