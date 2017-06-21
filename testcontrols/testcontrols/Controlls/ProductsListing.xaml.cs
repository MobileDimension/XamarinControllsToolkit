using System;
using System.Collections.Generic;
using testcontrols.ViewModels;
using Xamarin.Forms;

namespace testcontrols
{
    public partial class ProductsListing : ContentView
    {
        void Handle_Clicked1(object sender, System.EventArgs e)
        {
            if(sender is Button btn && btn.CommandParameter is string sku)
            {
                _viewModel.AddToBasket(sku);
            }
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        bool isBasketOpened = false;
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if(sender is Button btn)
            {
				if (!isBasketOpened)
				{
					var animate = new Animation(d => basketGrid.HeightRequest = d, basketGrid.Height, 600, Easing.CubicIn);
					animate.Commit(basketGrid, "ShowBasket", 32, 500);
					isBasketOpened = true;
                    btn.Text = "Hide";
                    _viewModel.IsBasketVisible = true;
				}
				else
				{
					var animate = new Animation(d => basketGrid.HeightRequest = d, basketGrid.Height, 70, Easing.CubicOut);
					animate.Commit(basketGrid, "HideBasket", 32, 500);
					isBasketOpened = false;
                    btn.Text = "More";
                    _viewModel.IsBasketVisible = false;
                }
            }
        }

        ProductsListingViewModel _viewModel;
        public ProductsListing()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new ProductsListingViewModel();
        }
    }
}
