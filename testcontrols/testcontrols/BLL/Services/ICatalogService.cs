using System;
using System.Collections.Generic;

namespace testcontrols.BLL.Services
{
    public interface ICatalogService
    {
	    event Action<string, string, string> OnProductInfoReceived;
	    event Action<string, int?> OnProductPriceReceived;
	    event Action<string, int?> OnProductRemainsReceived;
	    event Action<List<string>> OnCatalogueSkuReceived;

        void StartSearchSkus(string searchQuery);
        void StartLoadingProductInfo(string sku);
        void StartLoadingProductPrice(string sku);
        void StartLoadingProductRemains(string sku);
    }
}
