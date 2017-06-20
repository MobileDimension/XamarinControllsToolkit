using System;
using System.Collections.Generic;

namespace testcontrols.DAL.ResponseModels
{
    public class SearchProductsResponse : IResponseBody
    {
        public IEnumerable<string> Skus { get; set; }
    }
}
