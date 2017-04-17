using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace testcontrols
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var authorizationService = Core.DI.Container.GetInstance<IAuthorizationService>();
            authorizationService.AuthorizationChanged += AuthorizationService_AuthorizationChanged;
        }

        private void AuthorizationService_AuthorizationChanged(bool isAutorized)
        {
            if (!isAutorized)
            {
                Navigation.PushModalAsync(new LoginPage());
            }
        }
    }
}
