using System.Threading.Tasks;
using HaTeamsWatcher.Models;

namespace HaTeamsWatcher.Interfaces
{
    public interface IHomeAssistantService
    {
        Task<bool> Update(HomeAssistantStatus status);
    }
}