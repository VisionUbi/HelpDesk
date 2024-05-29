using System.Threading.Tasks;
using Abp.Application.Services;
using HelpDesk.Sessions.Dto;

namespace HelpDesk.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
