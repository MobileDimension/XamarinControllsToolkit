using System;
namespace testcontrols.DAL.ResponseModels
{
    public class BasketResponse : IResponseBody
    {
        public bool? Succseeded { get; set; }
        public string PositionId { get; set; }
        public string Sku { get; set; }
    }
}
