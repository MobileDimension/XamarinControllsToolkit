using System;
using testcontrols.DAL.ResponseModels;

namespace testcontrols.DAL
{
    public interface ICatalogRepository
    {
        Ticket<SearchProductsResponse> SearchProductsTicket(string searchQuery);
        Ticket<GetProductInfoResponse> GetInfoTicket(string sku);
        Ticket<GetProductPriceResponse> GetPriceTicket(string sku);
        Ticket<GetProductRemainsResponse> GetRemainsTicket(string sku);
        event Action<string, int?> OnProductRemainsChanging;
    }
}
