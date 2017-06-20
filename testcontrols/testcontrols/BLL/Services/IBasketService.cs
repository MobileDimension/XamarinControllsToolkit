using System;
using System.Collections.Generic;
using testcontrols.BLL.Models;

namespace testcontrols.BLL.Services
{
    public interface IBasketService
    {
        event Action<string, string> OnProductAddedSuccessfully;
        event Action<string> OnProductAddedFailure;
        void StartAddingProduct(string sku);
		event Action<string, string> OnProductRemoveSuccessfully;
		event Action<string> OnProductRemoveFailure;
		void StartRemovingProduct(string positionId);

        int? TotalCount { get; }
        int? TotalPrice { get; }
    }   
}
