using System;
namespace testcontrols.DAL.ResponseModels
{
    public class GetProductPriceResponse : IResponseBody
    {
        public string Sku { get; set; }
        public int? Price { get; set; }
        public int? DiscountPrice { get; set; }
    }
}
