using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private static readonly Lazy<AuthorizationService> _instance = new Lazy<AuthorizationService>(() => new AuthorizationService());
        public static AuthorizationService Instance => _instance.Value;
        public event IsAuthorizedDelegate AuthorizationChanged;
        public delegate void IsAuthorizedDelegate(bool isAutorized);

        public async Task Login(string login)
        {
            //await go to DAL
            //OnSuccess:
            await Task.Delay(3000);
            AuthorizationChanged.Invoke(true);
        }
        public async Task Logout()
        {
            // await go to server
            AuthorizationChanged.Invoke(false);
        }

    }
}
