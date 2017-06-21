using System;
using testcontrols.BLL.Enums;

namespace testcontrols.BLL.Models
{
    public class BasketProduct
    {
        public string Sku { get; set; }
        public int? Price { get; set; }
        public RequestState State { get; set; }
        public string PositionId { get; set; }
    }
}
