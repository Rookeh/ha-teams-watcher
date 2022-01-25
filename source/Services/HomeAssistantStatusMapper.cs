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
                        Name = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Available}:{Constants.Configuration.StatusMappings.StatusValues.Name}"),
                        Icon = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Available}:{Constants.Configuration.StatusMappings.StatusValues.Icon}")
                    };
                case TeamsStatus.Busy:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Busy}:{Constants.Configuration.StatusMappings.StatusValues.Name}"),
                        Icon = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Busy}:{Constants.Configuration.StatusMappings.StatusValues.Icon}")
                    };
                case TeamsStatus.OnACall:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.OnACall}:{Constants.Configuration.StatusMappings.StatusValues.Name}"),
                        Icon = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.OnACall}:{Constants.Configuration.StatusMappings.StatusValues.Icon}")
                    };
                case TeamsStatus.Away:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Away}:{Constants.Configuration.StatusMappings.StatusValues.Name}"),
                        Icon = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Away}:{Constants.Configuration.StatusMappings.StatusValues.Icon}")
                    };
                case TeamsStatus.BRB:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.BRB}:{Constants.Configuration.StatusMappings.StatusValues.Name}"),
                        Icon = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.BRB}:{Constants.Configuration.StatusMappings.StatusValues.Icon}")
                    };
                case TeamsStatus.DoNotDisturb:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.DoNotDisturb}:{Constants.Configuration.StatusMappings.StatusValues.Name}"),
                        Icon = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.DoNotDisturb}:{Constants.Configuration.StatusMappings.StatusValues.Icon}")
                    };
                case TeamsStatus.Focusing:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Focusing}:{Constants.Configuration.StatusMappings.StatusValues.Name}"),
                        Icon = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Focusing}:{Constants.Configuration.StatusMappings.StatusValues.Icon}")
                    };
                case TeamsStatus.Presenting:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Presenting}:{Constants.Configuration.StatusMappings.StatusValues.Name}"),
                        Icon = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Presenting}:{Constants.Configuration.StatusMappings.StatusValues.Icon}")
                    };
                case TeamsStatus.InAMeeting:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.InAMeeting}:{Constants.Configuration.StatusMappings.StatusValues.Name}"),
                        Icon = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.InAMeeting}:{Constants.Configuration.StatusMappings.StatusValues.Icon}")
                    };
                case TeamsStatus.Offline:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Offline}:{Constants.Configuration.StatusMappings.StatusValues.Name}"),
                        Icon = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Offline}:{Constants.Configuration.StatusMappings.StatusValues.Icon}")
                    };
                default:
                    return new HomeAssistantStatus
                    {
                        Name = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Unknown}:{Constants.Configuration.StatusMappings.StatusValues.Name}"),
                        Icon = _config.GetValue<string>($"{Constants.Configuration.StatusMappings.SectionName}:{Constants.Configuration.StatusMappings.Statuses.Unknown}:{Constants.Configuration.StatusMappings.StatusValues.Icon}")
                    };
            }
        }
    }
}