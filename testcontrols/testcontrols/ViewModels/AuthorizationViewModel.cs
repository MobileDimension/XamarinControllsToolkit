using BLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Xamarin.Forms;

namespace testcontrols.ViewModels
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        public ICommand RegisterCommand { protected set; get; }
        AuthorizationService _authorizationService;
        public AuthorizationViewModel()
        {
            this.RegisterCommand = new Command(async () => { await Registration(); });
            _authorizationService = Core.DI.Container.GetInstance<IAuthorizationService>() as AuthorizationService;
        }
        private async Task Registration()
        {
            IsLoading = true;
            await _authorizationService.Login(Login);
            IsLoading = false;
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
        
#region NotifyRealization
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaizePropertyChanged(params string[] propertyNames)
        {
            foreach(var name in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
#endregion
    }
}
