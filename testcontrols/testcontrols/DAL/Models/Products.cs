using System;
using System.Collections.Generic;
using System.Linq;

namespace testcontrols.DAL.Models
{
    public class Product 
    {
        public string Sku { get; set; }   
        public string Title { get; set; }
        public string ImageUri { get; set; }
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
                OnRemainChanged?.Invoke(this.Sku, this.Count);
            } 
        }
        public int? Price { get; set; }
        public int? DiscountPrice { get; set; }
        public string DeliveryDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, DateTime.Now.Day).ToString();
        public event Action<string, int?> OnRemainChanged;
    }

    public class CataloguePartition
    {
        public string PartitionId { get; set; }
        public string GroupQuery { get; set; }
        public string Title { get; set; }
    }

    public interface IProducts
    {
        List<string> GetSkus(string searchQuery);
        Product GetProduct(string sku);
        List<CataloguePartition> GetPartitions();
        event Action<string, int?> OnProductRemainsChanged;
    }

    public class ProductsMoq : IProducts
	{
		private List<Product> _products { get; set; }
		private List<CataloguePartition> _partitions { get; set; }

        private Dictionary<string, List<string>> _partitionProduct;

        public event Action<string, int?> OnProductRemainsChanged;

		public ProductsMoq()
		{
            InitPartitions();
            InitProducts();
            LinkPartitionProducts();
            foreach(var product in _products)
            {
                product.OnRemainChanged += (sku, count) => OnProductRemainsChanged?.Invoke(sku, count);    
            }
		}

        public List<string> GetSkus(string searchQuery)
        {
            return _partitionProduct[searchQuery];    
        }

        public Product GetProduct(string sku)
        {
            return _products.FirstOrDefault(x => x.Sku == sku);
        }

        public List<CataloguePartition> GetPartitions()
        {
            return _partitions;
        }

        private void LinkPartitionProducts()
        {
            _partitionProduct = new Dictionary<string, List<string>>()
            {
                {"0", new List<string>() {"100001", "100002", "100003", "100011", "100012", "100013", "100021", "100022", "100023", "100024"}},
                {"1", new List<string>() {"100001", "100002", "100003"}},
                {"2", new List<string>() {"100011", "100012", "100013"}},
                {"3", new List<string>() {"100021", "100022", "100023", "100024" }}
            };
        }

        private void InitPartitions()
        {
            _partitions = new List<CataloguePartition>()
            {
				new CataloguePartition()
				{
					PartitionId = "0", GroupQuery = "0", Title = "Все"
				},
                new CataloguePartition()
                {
                    PartitionId = "1", GroupQuery = "1", Title = "Телевизоры"
                },
				new CataloguePartition()
				{
					PartitionId = "2", GroupQuery = "2", Title = "Телефоны"
				},
				new CataloguePartition()
				{
					PartitionId = "3", GroupQuery = "3", Title = "Стиральные машины"
				}
            };
        }
        private void InitProducts()
        {
            _products = new List<Product>()
            {
                new Product()
                {
                    Sku = "100001", Title = "Samsung k200", Count = 20, ImageUri = "http://img.mvideo.ru/Pdb/small_pic/480/10011522b.jpg", Price = 24000, DiscountPrice = 22900
                },
				new Product()
				{
					Sku = "100002", Title = "Panasonic TX-40EXR600", Count = 15, ImageUri = "http://img.mvideo.ru/Pdb/small_pic/480/10012135b.jpg", Price = 18000, DiscountPrice = 15900
				},
				new Product()
				{
					Sku = "100003", Title = "Sony KDL40RD353", Count = 2, ImageUri = "http://img.mvideo.ru/Pdb/small_pic/480/10010406b.jpg", Price = 34000, DiscountPrice = 34000
				},
				new Product()
				{
					Sku = "100011", Title = "iPhone 7 Plus", Count = 12, ImageUri = "http://img.mvideo.ru/Pdb/small_pic/480/30026229b.jpg", Price = 48000, DiscountPrice = 47900
				},
				new Product()
				{
					Sku = "100012", Title = "Samsung galaxy s8", Count = 15, ImageUri = "http://img.mvideo.ru/Pdb/30027818m.jpg", Price = 42000, DiscountPrice = 40900
				},
				new Product()
				{
					Sku = "100013", Title = "Sony Xperia X", Count = 7, ImageUri = "http://img.mvideo.ru/Pdb/30025404m.jpg", Price = 49000, DiscountPrice = null
				},
				new Product()
                {
                    Sku = "100021", Title = "Indesit XWDA", Count = 5, ImageUri = "http://img.mvideo.ru/Pdb/small_pic/480/20033899b.jpg", Price = 35000, DiscountPrice = 34000
                },
                new Product()
				{
					Sku = "100022", Title = "LG F1296CD3", Count = 0, ImageUri = "http://img.mvideo.ru/Pdb/small_pic/480/20037902b.jpg", Price = 26000, DiscountPrice = null
				},
				new Product()
				{
					Sku = "100023", Title = "Gorenje W65Z03R/S", Count = null, ImageUri = "http://img.mvideo.ru/Pdb/small_pic/480/20028847b.jpg", Price = 21000, DiscountPrice = 21000
				},
				new Product()
				{
					Sku = "100024", Title = "Samsung WW80K52E61S", Count = 3, ImageUri = "http://img.mvideo.ru/Pdb/small_pic/480/20036821b.jpg", Price = 10200, DiscountPrice = 10300
				},
            };
        }
	}
}
