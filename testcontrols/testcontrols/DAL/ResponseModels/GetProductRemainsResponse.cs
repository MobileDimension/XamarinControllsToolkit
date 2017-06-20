using System;
namespace testcontrols.DAL.ResponseModels
{
    public class GetProductRemainsResponse : IResponseBody
    {
        public string Sku { get; set; }
        public int? Count { get; set; }
        public string DeliveryDate { get; set; }
    }
}
