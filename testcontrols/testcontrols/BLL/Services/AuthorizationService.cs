using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testcontrols.DAL;

namespace testcontrolls.BLL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public event Action<bool> AuthorizationChanged;

        private IClientRepository _clientRepository;

        public AuthorizationService()
        {
            _clientRepository = testcontrols.Core.DI.Container.GetInstance<IClientRepository>();    
        }

        public void Login(string login)
        {
            var loginTicket = _clientRepository.Auth(login);
            loginTicket.OnSuccess += (response) => 
            {
                AuthorizationChanged.Invoke(response.Data.IsAuthorized); 
            };
        }
        public void Logout()
        {
            // await go to server
            AuthorizationChanged.Invoke(false);
        }

    }
}
