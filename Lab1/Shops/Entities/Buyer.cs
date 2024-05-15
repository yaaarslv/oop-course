using Shops.Models;
using Shops.Tools;

namespace Shops.Entities
{
    public class Buyer
    {
        private const decimal _startMoney = 10000;
        private readonly Dictionary<Product, int> _purchasedProducts;
        public string Name { get; }
        public decimal Money { get; private set; }
        public IReadOnlyDictionary<Product?, int> PurchasedProducts => _purchasedProducts!;

        public Buyer(string name)
        {
            if (name is null)
            {
                throw new ShopNameIsNullException("BuyerName is null!");
            }

            _purchasedProducts = new Dictionary<Product, int>();
            Name = name;
            Money = _startMoney;
        }

        public decimal Purchase(decimal money)
        {
            Money -= money;
            return money;
        }

        public void AddPurchasedProduct(Product? product, int addingProductCount)
        {
            if (product is null)
            {
                throw new ProductIsNullException("Product is null!");
            }

            if (addingProductCount == 0)
            {
                throw new ZeroProductsException("Count of adding products is zero!");
            }

            if (_purchasedProducts.ContainsKey(product))
            {
                _purchasedProducts[product] += addingProductCount;
            }
            else
            {
                _purchasedProducts.Add(product, addingProductCount);
            }
        }
    }
}