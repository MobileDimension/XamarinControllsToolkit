using static BLL.Services.AuthorizationService;

namespace BLL.Services
{
    public interface IAuthorizationService
    {
        event IsAuthorizedDelegate IsAuthorized;
    }
}