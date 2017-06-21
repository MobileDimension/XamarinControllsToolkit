using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testcontrolls.BLL.Services;
using testcontrols.BLL.Services;
using testcontrols.DAL;
using testcontrols.DAL.Models;

namespace testcontrols.Core.DI
{
    public static class Bootstrapper
    {
        static Bootstrapper()
        {
        }

        public static void RegisterIoC()
        {
            //BLL services
            Container.Instance.RegisterSingleton(typeof(IAuthorizationService), null, typeof(AuthorizationService));
            Container.Instance.RegisterSingleton(typeof(ICatalogService), null, typeof(CatalogueService));
            Container.Instance.RegisterSingleton(typeof(IBasketService), null, typeof(BasketService));




			//DAL repositories
			Container.Instance.RegisterSingleton(typeof(IClientRepository), null, typeof(ClientRepository));
            Container.Instance.RegisterSingleton(typeof(ICatalogRepository), null, typeof(CatalogRepository));
            Container.Instance.RegisterSingleton(typeof(IBasketRepository), null, typeof(BasketRepository));

            //Data moqs
            Container.Instance.RegisterSingleton(typeof(IProducts), null, typeof(ProductsMoq));
        }
    }
}
