using Shops.Models;
using Shops.Services;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private readonly Dictionary<Product, int> _products;

        public int Id { get; }
        public string Name { get; }
        public string Address { get; }
        public IReadOnlyDictionary<Product?, int> Products => _products!;
        public decimal Money { get; private set; }

        public Shop(string name, string address)
        {
            if (name is null)
            {
                throw new ShopNameIsNullException("Name is null!");
            }

            if (address is null)
            {
                throw new NullReferenceException("Adress is null!");
            }

            Id = GetHashCode();
            _products = new Dictionary<Product, int>();
            Name = name;
            Address = address;
        }

        public Product? AddProduct(Product? product, int addingProductCount)
        {
            if (product is null)
            {
                throw new ProductIsNullException("Product is null!");
            }

            if (addingProductCount == 0)
            {
                throw new ZeroProductsException("Count of adding products is zero!");
            }

            if (_products.ContainsKey(product))
            {
                _products[product] += addingProductCount;
            }
            else
            {
                _products.Add(product, addingProductCount);
            }

            return product;
        }

        public Product? UpdatePrice(Product? product, int newPrice)
        {
            if (product is null)
            {
                throw new ProductIsNullException("Product is null!");
            }

            if (newPrice == 0)
            {
                throw new PriceIsZeroException("New price of product is zero!");
            }

            if (!_products.ContainsKey(product))
            {
                throw new ProductNotExistsException($"Can't update price because product doesn't exist in shop {Name}");
            }

            int productCount = _products[product];
            _products.Remove(product);
            var newProduct = new Product(product.Name, newPrice);
            _products.Add(newProduct, productCount);

            return newProduct;
        }

        public void Buy(Product? product, int buyingProductCount, Buyer buyer)
        {
            if (product is null)
            {
                throw new ProductIsNullException("Product is null!");
            }

            if (buyingProductCount == 0)
            {
                throw new ZeroProductsException("Count of buying products is zero!");
            }

            if (buyer is null)
            {
                throw new NullReferenceException("Buyer is null!");
            }

            if (!_products.ContainsKey(product))
            {
                throw new ProductNotExistsException($"Can't buy product because product doesn't exist in shop {Name}");
            }

            if (buyingProductCount > _products[product])
            {
                throw new TooManyBuyingProductsException($"Can't buy {buyingProductCount} units of product because there are only {_products[product]} units of product!");
            }

            decimal totalCost = buyingProductCount * product.Price;
            if (totalCost > buyer.Money)
            {
                throw new NoEnoughMoneyException($"There is not enough money to buy {buyingProductCount} units of goods!");
            }

            Money += buyer.Purchase(totalCost);
            buyer.AddPurchasedProduct(product, buyingProductCount);
            _products[product] -= buyingProductCount;
        }
    }
}