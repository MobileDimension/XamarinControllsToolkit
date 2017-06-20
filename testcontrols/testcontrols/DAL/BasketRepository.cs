using System;
using System.Collections.Generic;
using testcontrols.DAL.Models;
using testcontrols.DAL.ResponseModels;

namespace testcontrols.DAL
{
    public class BasketRepository : IBasketRepository
    {
        private IProducts _products;
        public BasketRepository()
        {
            _products = Core.DI.Container.GetInstance<IProducts>();
            _productsPositionIds = new Dictionary<string, string>();
        }



        private Dictionary<string, string> _productsPositionIds;

		public Ticket<BasketResponse> AddToBasket(string sku)
		{
			var response = new Response<BasketResponse>();
            var flag = false;
            var guid = "";
            var product = _products.GetProduct(sku);
            if (product != null && product?.Count > 0) 
            {
				product.Count -= 1;
                flag = true;
                guid = Guid.NewGuid().ToString();
                _productsPositionIds.Add(guid, sku);
            }

            response.Data = new BasketResponse() { 
                Succseeded = flag, 
                PositionId = flag ? guid : null,  
                Sku = sku
            };
			var ticket = new Ticket<BasketResponse>();
			ticket.Response = response;
			ticket.DoJob();
			return ticket;
		}

        public Ticket<BasketResponse> RemoveFromBasket(string positionId)
        {
			var response = new Response<BasketResponse>();
			var flag = false;
            var sku = _productsPositionIds[positionId];
            if(sku != null)
            {
				var product = _products.GetProduct(sku);
				if (product != null)
				{
					product.Count += 1;
					flag = true;
                    _productsPositionIds.Remove(positionId);
				}
            }
			response.Data = new BasketResponse()
			{
				Succseeded = flag,
                PositionId = positionId,
				Sku = sku
			};
			var ticket = new Ticket<BasketResponse>();
			ticket.Response = response;
			ticket.DoJob();
			return ticket;
        }
    }
}
