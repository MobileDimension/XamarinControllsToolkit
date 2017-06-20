using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using testcontrols.BLL.Services;
using testcontrols.Models;
using Xamarin.Forms;

namespace testcontrols.ViewModels
{
    public class ProductsListingViewModel : BaseViewModel
    {
        //public ICommand ProductSelectedCommand { protected set; get; }

        private ICatalogService _catalogueService;
        private IBasketService _basketService;

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products 
        { 
            get
            {
                return _products;
            }
            set
            {
                _products = value;
                RaizePropertyChanged(nameof(Products));
            }
        }


        public int? TotalCount
        {
            get
            {
                return _basketService.TotalCount;
            }
        }

        public int? TotalPrice
        {
            get
            {
                return _basketService.TotalPrice;
            }
        }

        private bool _isBasketVisible;
        public bool IsBasketVisible
        {
            get
            {
                return _isBasketVisible;
            }
            set
            {
                _isBasketVisible = value;
                RaizePropertyChanged(nameof(IsBasketVisible));
            }
        }

        public ProductsListingViewModel()
        {
            Products = new ObservableCollection<Product>();
            _catalogueService = Core.DI.Container.GetInstance<ICatalogService>();
            _basketService = Core.DI.Container.GetInstance<IBasketService>();

            _basketService.OnProductAddedSuccessfully += _basketService_OnProductAddedSuccessfully;
            _basketService.OnProductRemoveSuccessfully += _basketService_OnProductRemoveSuccessfully;
            _basketService.OnProductAddedFailure += _basketService_OnProductAddedFailure;
            _catalogueService.OnCatalogueSkuReceived += _catalogueService_OnCatalogueSkuReceived;
            _catalogueService.OnProductInfoReceived += _catalogueService_OnProductInfoReceived;
            _catalogueService.OnProductPriceReceived += _catalogueService_OnProductPriceReceived;
            _catalogueService.OnProductRemainsReceived += _catalogueService_OnProductRemainsReceived;
            InitProducts();
        }

        void _catalogueService_OnCatalogueSkuReceived(System.Collections.Generic.List<string> skus)
        {
            if (skus == null) return;
            foreach(var sku in skus)
            {
                Products.Add(new Product() { Sku = sku});
                RaizePropertyChanged(nameof(Products));
                _catalogueService.StartLoadingProductInfo(sku);
                _catalogueService.StartLoadingProductPrice(sku);
                _catalogueService.StartLoadingProductRemains(sku);
            }

        }

        void _catalogueService_OnProductInfoReceived(string sku, string title, string imageUri)
        {
            var product = Products.ToList().FirstOrDefault(x => x.Sku == sku);
            if(product != null)
            {
                product.Title = title;
                product.ImageUrl = imageUri;
            }
        }

        void _catalogueService_OnProductPriceReceived(string sku, int? price)
        {
			var product = Products.ToList().FirstOrDefault(x => x.Sku == sku);
			if (product != null)
			{
                product.Price = price;
			}
        }

        void _catalogueService_OnProductRemainsReceived(string sku, int? count)
        {
			var product = Products.ToList().FirstOrDefault(x => x.Sku == sku);
			if (product != null)
			{
                product.Count = count;
			}
        }

        private void InitProducts(){
			//Products = new ObservableCollection<Product>();
            _catalogueService.StartSearchSkus("0");
        }

        public void AddToBasket(string sku)
        {
            var product = Products.ToList().FirstOrDefault(x => x.Sku == sku);
            product.IsAddingInProfress = true;
            _basketService.StartAddingProduct(sku);
        }

        void _basketService_OnProductAddedSuccessfully(string sku, string positionId)
        {
            var product = Products.ToList().FirstOrDefault(x => x.Sku == sku);
            product.CountInBasket++;
            product.IsAddingInProfress = false;
            product.PositionIds.Add(positionId);
            RaizePropertyChanged(nameof(TotalCount), nameof(TotalPrice));
        }

        void _basketService_OnProductAddedFailure(string sku)
        {
			var product = Products.ToList().FirstOrDefault(x => x.Sku == sku);
			product.IsAddingInProfress = false;
        }

        void _basketService_OnProductRemoveSuccessfully(string sku, string positionId)
        {
			var product = Products.ToList().FirstOrDefault(x => x.Sku == sku);
			product.CountInBasket--;
            product.IsAddingInProfress = false;
            product.PositionIds.Remove(positionId);
            RaizePropertyChanged(nameof(TotalCount), nameof(TotalPrice));
        }
    }
}
