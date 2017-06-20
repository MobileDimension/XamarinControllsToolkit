using testcontrolls.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace testcontrols
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            var authorizationService = Core.DI.Container.GetInstance<IAuthorizationService>();
            authorizationService.AuthorizationChanged += AuthorizationService_AuthorizationChanged;
        }
        
        private async void AuthorizationService_AuthorizationChanged(bool isAutorized)
        {
            if (isAutorized)
            {
                await Navigation.PushModalAsync(new MainPage());
            }
        }
    }
}
