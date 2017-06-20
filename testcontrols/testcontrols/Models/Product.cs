using System;
using System.Collections.Generic;
using System.Linq;

namespace testcontrols.Models
{
    public class Product : ViewModels.BaseViewModel
    {
        private string _sku;
        public String Sku
        {
            get
            {
                return _sku;    
            }
            set
            {
                _sku = value;
                RaizePropertyChanged(nameof(Sku));
            }
        }

        public List<string> PositionIds { get; set; } = new List<string>();

        public string PositionId
        {
            get 
            {
                return PositionIds.LastOrDefault();
            }
        }

        public void UpdatePositionId()
        {
            RaizePropertyChanged(nameof(PositionId));
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaizePropertyChanged(nameof(Title));
            }
        }
        private int? _price;
        public int? Price
        {
            get
            {
                return _price;    
            }
            set
            {
                _price = value;
                RaizePropertyChanged(nameof(Price));
            }
        }
        private int? _count;
        public int? Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                RaizePropertyChanged(nameof(Count));
            }
        }
        private string _imageUrl;
        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }
            set
            {
                _imageUrl = value;
                RaizePropertyChanged(nameof(ImageUrl));
            }
        }

        private int _countInBasket;
        public int CountInBasket
        {
            get
            {
                return _countInBasket;
            }
            set
            {
                _countInBasket = value;
                RaizePropertyChanged(nameof(CountInBasket));
            }
        }
        public bool IsNotAdding
        {
            get 
            {
                return !IsAddingInProfress;
            }
        }

        private bool _isAddingInProgress;
        public bool IsAddingInProfress
        {
            get 
            {
                return _isAddingInProgress;
            }
            set
            {
                _isAddingInProgress = value;
                RaizePropertyChanged(nameof(IsAddingInProfress), nameof(IsNotAdding));
            }
        }

        public Product()
        {
            
        }
        public Product(string title, int? price, int? count)
        {
            Title = title;
            Price = price;
            Count = count ?? 0;
        }
    }
}
