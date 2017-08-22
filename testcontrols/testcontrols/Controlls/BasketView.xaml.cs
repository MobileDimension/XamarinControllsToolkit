using System;
using System.Collections.Generic;
using testcontrols.ViewModels;
using Xamarin.Forms;

namespace testcontrols
{
    public partial class BasketView : ContentView
    {
        private readonly BasketViewModel _basketViewModel;
        public BasketView()
        {
            InitializeComponent();
            this.BindingContext = _basketViewModel = new BasketViewModel();
        }

        void Add_Clicked(object sender, System.EventArgs e)
        {
			if (sender is Button btn && btn.CommandParameter is string sku)
			{
				_basketViewModel.AddProduct(sku);
			}

        }

        void Remove_Clicked(object sender, System.EventArgs e)
        {
			if (sender is Button btn && btn.CommandParameter is string positionId)
			{
                _basketViewModel.RemoveProduct(positionId);
			}
        }
    }
}
