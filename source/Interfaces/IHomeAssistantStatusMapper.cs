using HaTeamsWatcher.Models;

namespace HaTeamsWatcher.Interfaces
{
    public interface IHomeAssistantStatusMapper
    {
        HomeAssistantStatus MapFromTeamsStatus(TeamsStatus teamsStatus);
    }
}