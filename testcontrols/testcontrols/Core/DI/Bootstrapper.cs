using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testcontrols.Core.DI
{
    public static class Bootstrapper
    {
        static Bootstrapper()
        {
        }

        public static void RegisterIoC()
        {
            Container.Instance.RegisterSingleton(typeof(BLL.Services.IAuthorizationService), null, typeof(BLL.Services.AuthorizationService));
        }
    }
}
