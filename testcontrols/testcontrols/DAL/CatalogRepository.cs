using System;
using testcontrols.DAL.Models;
using testcontrols.DAL.ResponseModels;

namespace testcontrols.DAL
{
    public class CatalogRepository : ICatalogRepository
    {
        private IProducts _products;
        public CatalogRepository()
        {
            _products = Core.DI.Container.GetInstance<IProducts>();
			_products.OnProductRemainsChanged += (sku, count) => OnProductRemainsChanging?.Invoke(sku, count);
        }

        public event Action<string, int?> OnProductRemainsChanging;

        public Ticket<SearchProductsResponse> SearchProductsTicket(string searchQuery)
        {
			var response = new Response<SearchProductsResponse>();
            var productsSkus = _products.GetSkus(searchQuery);
			response.Data = new SearchProductsResponse() { Skus = productsSkus };
			var ticket = new Ticket<SearchProductsResponse>();
			ticket.Response = response;
			ticket.DoJob();
			return ticket;
        }

		public Ticket<GetProductInfoResponse> GetInfoTicket(string sku)
		{
			var response = new Response<GetProductInfoResponse>();
            var product = _products.GetProduct(sku);
            response.Data = new GetProductInfoResponse() { Sku = product.Sku, ImageUri = product.ImageUri, Title = product.Title  };
			var ticket = new Ticket<GetProductInfoResponse>();
			ticket.Response = response;
			ticket.DoJob();
			return ticket;
		}

		public Ticket<GetProductPriceResponse> GetPriceTicket(string sku)
		{
			var response = new Response<GetProductPriceResponse>();
			var product = _products.GetProduct(sku);
            response.Data = new GetProductPriceResponse() { Sku = product.Sku, DiscountPrice = product.DiscountPrice, Price = product.Price};
			var ticket = new Ticket<GetProductPriceResponse>();
			ticket.Response = response;
			ticket.DoJob();
			return ticket;
		}

		public Ticket<GetProductRemainsResponse> GetRemainsTicket(string sku)
		{
			var response = new Response<GetProductRemainsResponse>();
			var product = _products.GetProduct(sku);
            response.Data = new GetProductRemainsResponse() { Sku = product.Sku, Count = product.Count, DeliveryDate = product.DeliveryDate};
			var ticket = new Ticket<GetProductRemainsResponse>();
			ticket.Response = response;
			ticket.DoJob();
			return ticket;
		}


    }
}
