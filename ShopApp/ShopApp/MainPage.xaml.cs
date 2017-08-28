using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using testcontrols.Core.DI;
using testcontrols.BLL.Services;

namespace ShopApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var authService = Container
                .GetInstance<IAuthorizationService>();
            authService.AuthorizationChanged += AuthService_AuthorizationChanged;
            listing.AddProductRequest += Listing_AddProductRequest;
        }

        private async void Listing_AddProductRequest(string sku)
        {
            var basketService = Container.GetInstance<IBasketService>();
            if (_isAuth)
            {
                basketService.StartAddingProduct(sku);
                return;
            }
            await DisplayAlert("Авторизуйтесь", "Для добавления товара в корзину необходимо авторизоваться", "Хорошо");
        }

        private bool _isAuth = false;

        private void AuthService_AuthorizationChanged(bool isAuth)
        {
            _isAuth = isAuth;
            if (isAuth)
            {
                auth.IsVisible = false;
                basket.IsVisible = true;
            }
            else
            {
                auth.IsVisible = true;
                basket.IsVisible = false;
            }
        }

        private bool _isOpened = false;
        private void Handle_Clicked(object sender, EventArgs e)
        {
            if (_isOpened)
            {
                menu.WidthRequest = 0;
                _isOpened = false;
            }
            else
            {
                menu.WidthRequest = (this.Width * 15) / 16;
                _isOpened = true;
            }

        }
    }
}
