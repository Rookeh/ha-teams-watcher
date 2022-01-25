namespace HaTeamsWatcher.Models
{
    public static class Constants
    {
        public static class Authentication
        {
            public static class Schemes
            {
                public const string Basic = "Basic";
                public const string None = "None";
            }
        }

        public static class Configuration
        {
            public static class HomeAssistant
            {
                public const string SectionName = "HomeAssistant";
                public const string WebHookUrl = "WebHookUrl";

                public static class Authentication
                {
                    public const string SectionName = "Authentication";
                    public const string Scheme = "Scheme";
                }
            }

            public static class StatusMappings
            {
                public const string SectionName = "StatusMappings";

                public static class Statuses
                {
                    public const string Available = "Available";
                    public const string Busy = "Busy";
                    public const string OnACall = "OnACall";
                    public const string Away = "Away";
                    public const string BRB = "BRB";
                    public const string DoNotDisturb = "DoNotDisturb";
                    public const string Focusing = "Focusing";
                    public const string Presenting = "Presenting";
                    public const string InAMeeting = "InAMeeting";
                    public const string Offline = "Offline";
                    public const string Unknown = "Unknown";
                }

                public static class StatusValues
                {
                    public const string Name = "Name";
                    public const string Icon = "Icon";
                }
            }

            public static class Teams
            {
                public const string SectionName = "Teams";
                public const string LogFile = "LogFile";
                public const string Interval = "Interval";
            }
        }

        public static class Teams
        {
            public static class StatusIndicatorStateService
            {
                public const string NewActivityPrefix = "StatusIndicatorStateService: Added NewActivity";                
                public const string Prefix = "StatusIndicatorStateService: Added";
                public const string Available = "Available";
                public const string AvailableNewActivity = "NewActivity (current state: Available -> NewActivity";
                public const string Busy = "Busy";
                public const string BusyNewActivity = "NewActivity (current state: Busy -> NewActivity";
                public const string OnThePhone = "OnThePhone";
                public const string OnThePhoneNewActivity = "NewActivity (current state: OnThePhone -> NewActivity";
                public const string Away = "Away";
                public const string AwayNewActivity = "NewActivity (current state: Away -> NewActivity";
                public const string Brb = "BeRightBack";
                public const string BrbNewActivity = "NewActivity (current state: BeRightBack -> NewActivity";
                public const string Dnd = "DoNotDisturb";
                public const string DndNewActivity = "NewActivity (current state: DoNotDisturb -> NewActivity";
                public const string Focusing = "Focusing";
                public const string FocusingNewActivity = "NewActivity (current state: Focusing -> NewActivity";
                public const string Presenting = "Presenting";
                public const string PresentingNewActivity = "NewActivity (current state: Presenting -> NewActivity";
                public const string InAMeeting = "InAMeeting";
                public const string InAMeetingNewActivity = "NewActivity (current state: InAMeeting -> NewActivity";
                public const string Offline = "Offline";
            }

            public static class OverlayIcon
            {
                public const string NewActivity = "Setting the taskbar overlay icon - New activity";
                public const string Prefix = "Setting the taskbar overlay icon -";
                public const string BusyIcon = "Busy";
                public const string OnThePhoneIcon = "On the phone";
                public const string AwayIcon = "Away";
                public const string BrbIcon = "Be right back";
                public const string DndIcon = "Do not disturb";
                public const string PresentingIcon = "Presenting";
                public const string FocusingIcon = "Focusing";
                public const string InAMeeting = "In a meeting";
                public const string OfflineIcon = "Offline";
                public const string InACallIcon = "In a call";
            }
        }
    }
}