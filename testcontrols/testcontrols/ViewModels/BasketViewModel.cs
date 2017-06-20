using System;
using System.Collections.ObjectModel;
using System.Linq;
using testcontrols.BLL.Services;
using testcontrols.Models;

namespace testcontrols.ViewModels
{
    public class BasketViewModel : BaseViewModel
    {
        IBasketService _basketService;
        ICatalogService _catalogueService;
        public ObservableCollection<Product> Products { get; set; }
        public BasketViewModel()
        {
            Products = new ObservableCollection<Product>();
            _basketService = Core.DI.Container.GetInstance<IBasketService>();
            _catalogueService = Core.DI.Container.GetInstance<ICatalogService>();
            _basketService.OnProductAddedSuccessfully += _basketService_OnProductAddedSuccessfully;
            _basketService.OnProductRemoveSuccessfully += _basketService_OnProductRemoveSuccessfully;
            _basketService.OnProductAddedFailure += _basketService_OnProductAddedFailure;

            _catalogueService.OnProductInfoReceived += _catalogueService_OnProductInfoReceived;
        }

        public void RemoveProduct(string positionId)
        {
            _basketService.StartRemovingProduct(positionId);
            var currentProduct = Products.ToList().FirstOrDefault(x => x.PositionIds.Contains(positionId));
            currentProduct.IsAddingInProfress = true;
        }

        public void AddProduct(string sku)
        {
            _basketService.StartAddingProduct(sku);
			var currentProduct = Products.ToList().FirstOrDefault(x => x.Sku == sku);
			currentProduct.IsAddingInProfress = true;
        }

        void _basketService_OnProductAddedSuccessfully(string sku, string positionId)
        {
            var currentProduct = Products.ToList().FirstOrDefault(x => x.Sku == sku);
            if(currentProduct != null)
            {
                currentProduct.CountInBasket++;
                currentProduct.IsAddingInProfress = false;
                currentProduct.PositionIds.Add(positionId);
                currentProduct.UpdatePositionId();
            }
            else
            {
                var product = new Product()
                {
                    Sku = sku,
                };
                product.PositionIds.Add(positionId);
                product.UpdatePositionId();
                product.CountInBasket++;
				Products.Add(product);
				_catalogueService.StartLoadingProductInfo(sku); 
            }
        }

        void _basketService_OnProductAddedFailure(string sku)
        {
            var currentProduct = Products.ToList().FirstOrDefault(x => x.Sku == sku);
            currentProduct.IsAddingInProfress = false;
        }

        void _basketService_OnProductRemoveSuccessfully(string sku, string positionId)
        {
            var currentProduct = Products.ToList().FirstOrDefault(x => x.Sku == sku);
            if (currentProduct == null) return;
            currentProduct.IsAddingInProfress = false;
            currentProduct.PositionIds.Remove(positionId);
            currentProduct.UpdatePositionId();
            if(currentProduct.CountInBasket == 1)
            {
                Products.Remove(currentProduct);
            }
            currentProduct.CountInBasket--;
        }

        void _catalogueService_OnProductInfoReceived(string sku, string title, string imageUrl)
        {
            var product = Products.ToList().FirstOrDefault(x => x.Sku == sku);
            if (product == null) return;
            product.Title = title;
            product.ImageUrl = imageUrl;
        }
    }
}
