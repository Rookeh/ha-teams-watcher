using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using HaTeamsWatcher.Interfaces;
using HaTeamsWatcher.Models;

namespace HaTeamsWatcher.Services
{
    public class TeamsStatusService : ITeamsStatusService
    {
        private readonly IConfiguration _config;

        public TeamsStatusService(IConfiguration config)
        {
            _config = config;
        }

        public TeamsStatus GetCurrentStatus()
        {
            var logFilePath = _config.GetSection(Constants.Configuration.Teams.SectionName)
                .GetValue<string>(Constants.Configuration.Teams.LogFile);

            var logFileContent = File.ReadAllLines(logFilePath);

            var lastStatusUpdate = logFileContent.LastOrDefault(l => l.Contains(Constants.Teams.StatusIndicatorStateService.Prefix) || l.Contains(Constants.Teams.OverlayIcon.Prefix));

            switch (lastStatusUpdate)
            {
                case string available when available.Contains(Constants.Teams.StatusIndicatorStateService.Available):
                case string availableNewActivity when availableNewActivity.Contains(Constants.Teams.StatusIndicatorStateService.AvailableNewActivity):
                    return TeamsStatus.Available;
                case string busy when busy.Contains(Constants.Teams.StatusIndicatorStateService.Busy):
                //case string busyNewActivity when busyNewActivity.Contains(Constants.Teams.StatusIndicatorStateService.BusyNewActivity):
                case string busyIcon when busyIcon.Contains(Constants.Teams.OverlayIcon.BusyIcon):
                    return TeamsStatus.Busy;
                case string onThePhone when onThePhone.Contains(Constants.Teams.StatusIndicatorStateService.OnThePhone):
                //case string onPhoneNewActivity when onPhoneNewActivity.Contains(Constants.Teams.StatusIndicatorStateService.OnThePhoneNewActivity):
                case string onPhoneIcon when onPhoneIcon.Contains(Constants.Teams.OverlayIcon.OnThePhoneIcon):
                    return TeamsStatus.OnACall;
                case string away when away.Contains(Constants.Teams.StatusIndicatorStateService.Away):
                //case string awayNewActivity when awayNewActivity.Contains(Constants.Teams.StatusIndicatorStateService.AwayNewActivity):
                case string awayIcon when awayIcon.Contains(Constants.Teams.OverlayIcon.AwayIcon):
                    return TeamsStatus.Away;
                case string brb when brb.Contains(Constants.Teams.StatusIndicatorStateService.Brb):
                //case string brbNewActivity when brbNewActivity.Contains(Constants.Teams.StatusIndicatorStateService.BrbNewActivity):
                case string brbIcon when brbIcon.Contains(Constants.Teams.OverlayIcon.BrbIcon):
                    return TeamsStatus.BRB;
                case string dnd when dnd.Contains(Constants.Teams.StatusIndicatorStateService.Dnd):
                //case string dndNewActivity when dndNewActivity.Contains(Constants.Teams.StatusIndicatorStateService.DndNewActivity):
                case string dndIcon when dndIcon.Contains(Constants.Teams.OverlayIcon.DndIcon):
                    return TeamsStatus.DoNotDisturb;
                case string focusing when focusing.Contains(Constants.Teams.StatusIndicatorStateService.Focusing):
                //case string focusingNewActivity when focusingNewActivity.Contains(Constants.Teams.StatusIndicatorStateService.FocusingNewActivity):
                case string focusingIcon when focusingIcon.Contains(Constants.Teams.OverlayIcon.FocusingIcon):
                    return TeamsStatus.Focusing;
                case string presenting when presenting.Contains(Constants.Teams.StatusIndicatorStateService.Presenting):
                //case string presentingNewActivity when presentingNewActivity.Contains(Constants.Teams.StatusIndicatorStateService.PresentingNewActivity):
                case string presentingIcon when presentingIcon.Contains(Constants.Teams.OverlayIcon.PresentingIcon):
                    return TeamsStatus.Presenting;
                case string inAMeeting when inAMeeting.Contains(Constants.Teams.StatusIndicatorStateService.InAMeeting):
                //case string inAMeetingNewActivity when inAMeetingNewActivity.Contains(Constants.Teams.StatusIndicatorStateService.InAMeetingNewActivity):
                case string inAMeetingIcon when inAMeetingIcon.Contains(Constants.Teams.OverlayIcon.InAMeeting):
                    return TeamsStatus.InAMeeting;
                case string offline when offline.Contains(Constants.Teams.StatusIndicatorStateService.Offline):
                case string offlineIcon when offlineIcon.Contains(Constants.Teams.OverlayIcon.OfflineIcon):
                    return TeamsStatus.Offline;
                default:
                    return TeamsStatus.Unknown;
            }
        }
    }
}