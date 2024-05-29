using System.Threading.Tasks;
using HelpDesk.Configuration.Dto;

namespace HelpDesk.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
