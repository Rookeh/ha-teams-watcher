using Microsoft.Extensions.Configuration;
using HaTeamsWatcher.Interfaces;
using HaTeamsWatcher.Models;

namespace HaTeamsWatcher.Services
{
    public class HomeAssistantStatusMapper : IHomeAssistantStatusMapper
    {
        private readonly IConfiguration _config;

        public HomeAssistantStatusMapper(IConfiguration config)
        {
            _config = config;
        }

        public HomeAssistantStatus MapFromTeamsStatus(TeamsStatus teamsStatus)
        {
            switch (teamsStatus)
            {
                case TeamsStatus.Available:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Available)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Name),
                        Icon = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Available)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Icon),
                    };
                case TeamsStatus.Busy:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Busy)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Name),
                        Icon = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Busy)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Icon),
                    };
                case TeamsStatus.OnACall:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.OnACall)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Name),
                        Icon = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.OnACall)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Icon),
                    };
                case TeamsStatus.Away:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Away)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Name),
                        Icon = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Away)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Icon),
                    };
                case TeamsStatus.BRB:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.BRB)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Name),
                        Icon = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.BRB)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Icon),
                    };
                case TeamsStatus.DoNotDisturb:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.DoNotDisturb)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Name),
                        Icon = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.DoNotDisturb)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Icon),
                    };
                case TeamsStatus.Focusing:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Focusing)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Name),
                        Icon = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Focusing)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Icon),
                    };
                case TeamsStatus.Presenting:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Presenting)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Name),
                        Icon = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Presenting)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Icon),
                    };
                case TeamsStatus.InAMeeting:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.InAMeeting)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Name),
                        Icon = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.InAMeeting)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Icon),
                    };
                case TeamsStatus.Offline:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Offline)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Name),
                        Icon = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Offline)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Icon),
                    };
                default:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Unknown)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Name),
                        Icon = _config.GetSection(Constants.Configuration.StatusMappings.SectionName)
                            .GetSection(Constants.Configuration.StatusMappings.Statuses.Unknown)
                            .GetValue<string>(Constants.Configuration.StatusMappings.StatusValues.Icon),
                    };
            }
        }
    }
}