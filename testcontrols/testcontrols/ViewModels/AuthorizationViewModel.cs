using testcontrolls.BLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace testcontrols.ViewModels
{
    public class AuthorizationViewModel : BaseViewModel
    {
        public ICommand RegistrationCommand { protected set; get; }
        AuthorizationService _authorizationService;
        public AuthorizationViewModel()
        {
            this.RegistrationCommand = new Command(() => { Registration(); });
            _authorizationService = Core.DI.Container.GetInstance<IAuthorizationService>() as AuthorizationService;
            _authorizationService.AuthorizationChanged += _authorizationService_AuthorizationChanged;
        }

        void _authorizationService_AuthorizationChanged(bool isAuthorized)
        {
            IsLoading = false;
        }

        private void Registration()
        {
            IsLoading = true;
            _authorizationService.Login(Login);
        }
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    RaizePropertyChanged(nameof(IsLoading));
                }
            }
        }

        private string _login;
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                if(_login != value)
                {
                    _login = value;
                    RaizePropertyChanged(nameof(Login));
                }
            }
        }

    }
}
