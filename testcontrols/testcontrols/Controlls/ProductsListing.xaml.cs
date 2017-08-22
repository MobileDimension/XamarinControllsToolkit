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
        ProductsListingViewModel _viewModel;
        public ProductsListing()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new ProductsListingViewModel();
        }
    }
}
