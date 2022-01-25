using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using HaTeamsWatcher.Interfaces;
using HaTeamsWatcher.Models;
using System.Collections.Generic;

namespace HaTeamsWatcher.Services
{
    public class TeamsStatusService : ITeamsStatusService
    {
        private readonly IConfiguration _config;
        private readonly IFile _file;

        public TeamsStatusService(IConfiguration config, IFile file)
        {
            _config = config;
            _file = file;
        }

        public TeamsStatus GetCurrentStatus()
        {
            var logFilePath = _config.GetValue<string>($"{Constants.Configuration.Teams.SectionName}:{Constants.Configuration.Teams.LogFile}");
            var logFileContent = GetLogFileContent(logFilePath);

            var lastStatusUpdate = logFileContent.LastOrDefault(l => (l.Contains(Constants.Teams.StatusIndicatorStateService.Prefix) && !l.Contains(Constants.Teams.StatusIndicatorStateService.NewActivityPrefix))
                || (l.Contains(Constants.Teams.OverlayIcon.Prefix) && !l.Contains(Constants.Teams.OverlayIcon.NewActivity)));

            switch (lastStatusUpdate)
            {
                case string available when available.Contains(Constants.Teams.StatusIndicatorStateService.Available):
                case string availableNewActivity when availableNewActivity.Contains(Constants.Teams.StatusIndicatorStateService.AvailableNewActivity):
                    return TeamsStatus.Available;
                case string busy when busy.Contains(Constants.Teams.StatusIndicatorStateService.Busy):
                case string busyIcon when busyIcon.Contains(Constants.Teams.OverlayIcon.BusyIcon):
                    return TeamsStatus.Busy;
                case string onThePhone when onThePhone.Contains(Constants.Teams.StatusIndicatorStateService.OnThePhone):
                case string onPhoneIcon when onPhoneIcon.Contains(Constants.Teams.OverlayIcon.OnThePhoneIcon):
                case string inACallIcon when inACallIcon.Contains(Constants.Teams.OverlayIcon.InACallIcon):
                    return TeamsStatus.OnACall;
                case string away when away.Contains(Constants.Teams.StatusIndicatorStateService.Away):
                case string awayIcon when awayIcon.Contains(Constants.Teams.OverlayIcon.AwayIcon):
                    return TeamsStatus.Away;
                case string brb when brb.Contains(Constants.Teams.StatusIndicatorStateService.Brb):
                case string brbIcon when brbIcon.Contains(Constants.Teams.OverlayIcon.BrbIcon):
                    return TeamsStatus.BRB;
                case string dnd when dnd.Contains(Constants.Teams.StatusIndicatorStateService.Dnd):
                case string dndIcon when dndIcon.Contains(Constants.Teams.OverlayIcon.DndIcon):
                    return TeamsStatus.DoNotDisturb;
                case string focusing when focusing.Contains(Constants.Teams.StatusIndicatorStateService.Focusing):
                case string focusingIcon when focusingIcon.Contains(Constants.Teams.OverlayIcon.FocusingIcon):
                    return TeamsStatus.Focusing;
                case string presenting when presenting.Contains(Constants.Teams.StatusIndicatorStateService.Presenting):
                case string presentingIcon when presentingIcon.Contains(Constants.Teams.OverlayIcon.PresentingIcon):
                    return TeamsStatus.Presenting;
                case string inAMeeting when inAMeeting.Contains(Constants.Teams.StatusIndicatorStateService.InAMeeting):
                case string inAMeetingIcon when inAMeetingIcon.Contains(Constants.Teams.OverlayIcon.InAMeeting):
                    return TeamsStatus.InAMeeting;
                case string offline when offline.Contains(Constants.Teams.StatusIndicatorStateService.Offline):
                case string offlineIcon when offlineIcon.Contains(Constants.Teams.OverlayIcon.OfflineIcon):
                    return TeamsStatus.Offline;
                default:
                    return TeamsStatus.Unknown;
            }
        }

        private List<string> GetLogFileContent(string logFilePath)
        {
            var logFileContent = new List<string>();
            using (var stream = _file.Open(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    logFileContent.Add(line);
                }
            }

            return logFileContent;
        }

    }
}