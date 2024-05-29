using System.Threading.Tasks;
using Abp.Application.Services;
using HelpDesk.Authorization.Accounts.Dto;

namespace HelpDesk.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
