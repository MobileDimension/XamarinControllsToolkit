using System;
using System.Threading.Tasks;

namespace testcontrolls.BLL.Services
{
    public interface IAuthorizationService
    {
        event Action<bool> AuthorizationChanged;
        void Login(string login);
        void Logout();
	}
}