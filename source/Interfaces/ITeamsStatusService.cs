using HaTeamsWatcher.Models;

namespace HaTeamsWatcher.Interfaces
{
    public interface ITeamsStatusService
    {
        TeamsStatus GetCurrentStatus();
    }
}