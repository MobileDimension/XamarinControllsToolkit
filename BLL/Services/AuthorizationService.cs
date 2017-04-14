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
        public event IsAuthorizedDelegate IsAuthorized;
        public delegate void IsAuthorizedDelegate();

        public async Task Authorize()
        {
            IsAuthorized.Invoke();
        }
    }
}
