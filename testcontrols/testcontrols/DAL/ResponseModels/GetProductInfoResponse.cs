using System;
namespace testcontrols.DAL.ResponseModels
{
    public class GetProductInfoResponse : IResponseBody
    {
        public string Sku { get; set; }
        public string ImageUri { get; set; }
        public string Title { get; set; }
    }
}
