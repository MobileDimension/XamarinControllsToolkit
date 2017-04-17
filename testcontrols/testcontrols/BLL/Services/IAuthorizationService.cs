using System.Threading.Tasks;
using static BLL.Services.AuthorizationService;

namespace BLL.Services
{
    public interface IAuthorizationService
    {
        event IsAuthorizedDelegate AuthorizationChanged;
    }
}