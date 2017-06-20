using System;
using testcontrols.DAL.ResponseModels;

namespace testcontrols.DAL
{
    public interface IBasketRepository
    {
        Ticket<BasketResponse> AddToBasket(string sku);
        Ticket<BasketResponse> RemoveFromBasket(string positionId);
    }
}
