using System;
using System.Collections.Generic;
using System.Linq;
using testcontrols.BLL.Models;
using testcontrols.DAL;

namespace testcontrols.BLL.Services
{
    public class CatalogueService : ICatalogService
    {
        private ICatalogRepository _catalogRepository;
        public CatalogueService()
        {
            _products = new List<CatalogueProduct>();
            _catalogRepository = Core.DI.Container.GetInstance<ICatalogRepository>();
            _catalogRepository.OnProductRemainsChanging += (sku, count) => OnProductRemainsReceived(sku, count);

        }
        public event Action<string, string, string> OnProductInfoReceived;
		public event Action<string, int?> OnProductPriceReceived;
		public event Action<string, int?> OnProductRemainsReceived;
        public event Action<List<string>> OnCatalogueSkuReceived;

		private List<CatalogueProduct> _products;

		public void StartSearchSkus(string searchQuery)
        {
            var ticket = _catalogRepository.SearchProductsTicket(searchQuery);
			ticket.OnSuccess += (response) =>
			{
                if(response.Data.Skus != null)
                {
                    foreach(var sku in response.Data.Skus)
                    {
                        _products.Add(new CatalogueProduct() {Sku = sku});
                    }
                }
                OnCatalogueSkuReceived.Invoke(response.Data.Skus.ToList());
			};
        }

        public void StartLoadingProductInfo(string sku)
        {
            var thisProduct = _products.FirstOrDefault(x => x.Sku == sku);
            if(thisProduct != null && thisProduct.InfoState == Enums.RequestState.Succseeded)
            {
                OnProductInfoReceived?.Invoke(sku, thisProduct.Name, thisProduct.ImageUrl);    
            }
            var tiket = _catalogRepository.GetInfoTicket(sku);
            tiket.OnSuccess += (response) => 
            {
                if (response.Data?.Sku != null)
                {
                    if(thisProduct == null) 
                    {
                        thisProduct = new CatalogueProduct() { Sku = sku };
                        _products.Add(thisProduct);
                    }
                    thisProduct.Name = response.Data.Title;
                    thisProduct.ImageUrl = response.Data.ImageUri;
                    thisProduct.InfoState = Enums.RequestState.Succseeded;
                    OnProductInfoReceived?.Invoke(thisProduct.Sku, thisProduct.Name, thisProduct.ImageUrl);
                    return;
                }
                OnProductInfoReceived?.Invoke(null, null, null);

            };
        }

        public void StartLoadingProductPrice(string sku)
        {
			var thisProduct = _products.FirstOrDefault(x => x.Sku == sku);
            if (thisProduct != null && thisProduct.PriceState == Enums.RequestState.Succseeded)
			{
                OnProductPriceReceived?.Invoke(sku, thisProduct.Price);
			}
			var tiket = _catalogRepository.GetPriceTicket(sku);
			tiket.OnSuccess += (response) =>
			{
				if (response.Data?.Sku != null)
				{
					if (thisProduct == null)
					{
						thisProduct = new CatalogueProduct() { Sku = sku };
						_products.Add(thisProduct);
					}
                    thisProduct.Price = response.Data.Price;
                    thisProduct.PriceState = Enums.RequestState.Succseeded;
                    OnProductPriceReceived?.Invoke(thisProduct.Sku, thisProduct.Price);
					return;
				}
				OnProductPriceReceived?.Invoke(null, null);
            };
        }

        public void StartLoadingProductRemains(string sku)
        {
			var thisProduct = _products.FirstOrDefault(x => x.Sku == sku);
            if (thisProduct != null && thisProduct.CountState == Enums.RequestState.Succseeded)
			{
                OnProductRemainsReceived?.Invoke(sku, thisProduct.Count);
			}
			var tiket = _catalogRepository.GetRemainsTicket(sku);
			tiket.OnSuccess += (response) =>
			{
				if (response.Data?.Sku != null)
				{
					if (thisProduct == null)
					{
						thisProduct = new CatalogueProduct() { Sku = sku };
						_products.Add(thisProduct);
					}
                    thisProduct.Count = response.Data.Count;
					thisProduct.CountState = Enums.RequestState.Succseeded;
                    OnProductRemainsReceived?.Invoke(thisProduct.Sku, thisProduct.Count);
					return;
				}
				OnProductPriceReceived?.Invoke(null, null);
			};
        }
    }
}
