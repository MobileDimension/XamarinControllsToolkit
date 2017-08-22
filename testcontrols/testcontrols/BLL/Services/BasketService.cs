using System;
using System.Collections.Generic;
using System.Linq;
using testcontrols.BLL.Models;
using testcontrols.DAL;

namespace testcontrols.BLL.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly List<BasketProduct> _products;
        public event Action<string, string> OnProductAddedSuccessfully;
        public event Action<string> OnProductAddedFailure;
		public event Action<string, string> OnProductRemoveSuccessfully;
		public event Action<string> OnProductRemoveFailure;

        public int? TotalCount => _products.Count;

        public int? TotalPrice
		{
			get
			{
				return _products.Where(x => x.State == Enums.RequestState.Succseeded).Sum(x => x.Price);
			}
		}

        public BasketService()
        {
            _products = new List<BasketProduct>();
            _basketRepository = Core.DI.Container.GetInstance<IBasketRepository>();
            _catalogRepository = Core.DI.Container.GetInstance<ICatalogRepository>();
        }

        public void StartAddingProduct(string sku)
        {
            var newProduct = new BasketProduct() { Sku = sku, State = Enums.RequestState.InProgress };
            _products.Add(newProduct);
            var tiket = _basketRepository.AddToBasket(sku);
            tiket.OnSuccess += (response) => 
            {
                if(response.Data.Succseeded != null && response.Data.Succseeded.Value)
                {
                    newProduct.PositionId = response.Data.PositionId;
                    newProduct.State = Enums.RequestState.Succseeded;
                    OnProductAddedSuccessfully?.Invoke(newProduct.Sku, newProduct.PositionId);
                }
                else
                {
                    OnProductAddedFailure?.Invoke(sku);
                    newProduct.State = Enums.RequestState.Failed;
                }
            };
            var priceTicket = _catalogRepository.GetPriceTicket(sku);
            priceTicket.OnSuccess += (response) => 
            {
                if(response.Data != null){
                    newProduct.Price = response.Data.Price;
                }
            };

        }

        public void StartRemovingProduct(string positionId)
        {
            var tiket = _basketRepository.RemoveFromBasket(positionId);
			tiket.OnSuccess += (response) =>
			{
				if (response.Data.Succseeded != null && response.Data.Succseeded.Value)
				{
                    OnProductRemoveSuccessfully?.Invoke(response.Data.Sku, response.Data.PositionId);
                    var thisProduct = _products.FirstOrDefault(x => x.PositionId != null);
                    if (thisProduct != null)
                    {
                        _products.Remove(thisProduct);   
                    }
				}
				else
				{
                    OnProductRemoveFailure?.Invoke(positionId);
                }
			};
        }


    }
}
