using Shops.Entities;
using Shops.Models;
using Shops.Tools;

namespace Shops.Services
{
    public class Manager
    {
        private readonly List<Shop> _shops;

        public Manager()
        {
            _shops = new List<Shop>();
        }

        public Shop CreateShop(string shopName, string shopAddress)
        {
            if (_shops.Any(shop => shop.Name == shopName && shop.Address == shopAddress))
            {
                throw new ShopAlreadyExistsException($"The shop {shopName} is already created!");
            }

            Shop newShop = new Shop(shopName, shopAddress);
            _shops.Add(newShop);
            return newShop;
        }

        public Product? CreateProduct(string productName, decimal productPrice)
        {
            Product? newProduct = new Product(productName, productPrice);
            return newProduct;
        }

        public Shop FindProfitableShop(Product? product, int productCount)
        {
            if (product is null)
            {
                throw new ProductIsNullException("Product is null!");
            }

            if (productCount == 0)
            {
                throw new ZeroProductsException("Count of finding products is zero!");
            }

            var shops = _shops.SelectMany(s => s.Products.Keys, (s, product1) => new { s, product1 })
                .Where(p => p.product1?.Name == product.Name)
                .Select(p => p.s)
                .OrderBy(s => s.Products.Keys.Min(p =>
                {
                    if (p != null) return p.Price;
                    return 0;
                }));

            if (shops.Count() == 0)
            {
                throw new NoProductInAnyShopException($"There is no product {product.Name} in any shop!");
            }

            var findingProduct = shops.SelectMany(shop => shop.Products)
                .FirstOrDefault(prod => prod.Key?.Name == product.Name && prod.Value >= productCount);

            if (findingProduct.Key is null && findingProduct.Value == 0)
            {
                throw new NoShopContainsSoManyProducts($"There is not a single shop that has {productCount} products!");
            }

            var shop = shops.FirstOrDefault(s => s.Products.ContainsKey(findingProduct.Key));

            return shop;
        }
    }
}