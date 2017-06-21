using System;
using testcontrols.BLL.Enums;

namespace testcontrols.BLL.Models
{
    public class CatalogueProduct
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? Price { get; set; }
        public int? Count { get; set; }
        public RequestState InfoState { get; set; }
        public RequestState PriceState { get; set; }
        public RequestState CountState { get; set; }

        public CatalogueProduct()
        {
            InfoState = RequestState.InProgress;
            PriceState = RequestState.InProgress;
            CountState = RequestState.InProgress;
        }
    }
}
