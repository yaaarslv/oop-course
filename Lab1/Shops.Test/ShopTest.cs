using Shops.Entities;
using Shops.Models;
using Shops.Services;
using Shops.Tools;
using Xunit;

namespace Shops.Tests
{
    public class ShopTest
    {
        private Manager _manager = new Manager();

        [Fact]
        public void AddProductsToShop_ShopHasProduct()
        {
            Shop shop = _manager.CreateShop("Пятёрочка", "Невский проспект, д. 146");
            Product? bread = _manager.CreateProduct("Хлебушек", 60);
            shop.AddProduct(bread, 20);
            Assert.Contains(bread, shop.Products);
        }

        [Fact]
        public void SetAndUpdatePrice()
        {
            Shop shop = _manager.CreateShop("Пятёрочка", "Невский проспект, д. 146");
            int startPrice = 60;
            Product? bread = _manager.CreateProduct("Хлебушек", startPrice);
            shop.AddProduct(bread, 10);
            Assert.True(bread.Price == startPrice);

            int updatedPrice = 70;
            Product? newBread = shop.UpdatePrice(bread, updatedPrice);
            Assert.True(newBread.Price == updatedPrice);
        }

        [Fact]
        public void FindShopWithMinPrice_ShopExists()
        {
            Shop testShop1 = _manager.CreateShop("Пятёрочка", "Невский проспект, д. 146");
            Shop testShop2 = _manager.CreateShop("Магнит", "ул. Моховая, д. 37");
            Product? bread1 = _manager.CreateProduct("Хлебушек", 40);
            testShop1.AddProduct(bread1, 5);

            Product? bread2 = _manager.CreateProduct("Хлебушек", 50);
            testShop2.AddProduct(bread2, 6);

            int testUnusedBreadPrice = 10;
            Product? bread = _manager.CreateProduct("Хлебушек", testUnusedBreadPrice);
            var shop = _manager.FindProfitableShop(bread, 6);
            Assert.True(shop == testShop2);
        }

        [Fact]
        public void FindShopWithMinPrice_NoProductInAnyShop_ThrowException()
        {
            Shop testShop1 = _manager.CreateShop("Пятёрочка", "Невский проспект, д. 146");
            Shop testShop2 = _manager.CreateShop("Магнит", "ул. Моховая, д. 37");
            Product? milk = _manager.CreateProduct("Молочко", 70);
            testShop1.AddProduct(milk, 9);

            Product? pineapple = _manager.CreateProduct("Ананасик", 555);
            testShop2.AddProduct(pineapple, 4);

            int testUnusedBreadPrice = 10;
            Product? bread = _manager.CreateProduct("Хлебушек", testUnusedBreadPrice);

            Assert.Throws<NoProductInAnyShopException>(() => _manager.FindProfitableShop(bread, 3));
        }

        [Fact]
        public void FindShopWithMinPrice_NoShopContainsSoManyProducts_ThrowException()
        {
            Shop testShop1 = _manager.CreateShop("Пятёрочка", "Невский проспект, д. 146");
            Shop testShop2 = _manager.CreateShop("Магнит", "ул. Моховая, д. 37");
            Product? bread1 = _manager.CreateProduct("Хлебушек", 40);
            testShop1.AddProduct(bread1, 9);

            Product? bread2 = _manager.CreateProduct("Хлебушек", 50);
            testShop2.AddProduct(bread2, 6);

            int testUnusedBreadPrice = 10;
            Product? bread = _manager.CreateProduct("Хлебушек", testUnusedBreadPrice);

            Assert.Throws<NoShopContainsSoManyProducts>(() => _manager.FindProfitableShop(bread, 10));
        }

        [Fact]
        public void BuyProducts_BuyerMoneyDecrements_ShopMoneyIncrements()
        {
            Shop testShop = _manager.CreateShop("Пятёрочка", "Невский проспект, д. 146");
            int productPrice = 45;
            decimal shopMoneyBefore = testShop.Money;
            Product? bread = _manager.CreateProduct("Хлебушек", productPrice);

            int productBefore = 10;
            testShop.AddProduct(bread, productBefore);

            Buyer buyer = new Buyer("Василий Петрович Голобородько");
            decimal buyerMoneyBefore = buyer.Money;

            int buyingProductCount = 5;
            testShop.Buy(bread, buyingProductCount, buyer);

            Assert.True(testShop.Money == shopMoneyBefore + (productPrice * buyingProductCount));
            Assert.True(buyer.Money == buyerMoneyBefore - (productPrice * buyingProductCount));
            Assert.True(testShop.Products[bread] == productBefore - buyingProductCount);
            Assert.True(buyer.PurchasedProducts.ContainsKey(bread));
            Assert.True(buyer.PurchasedProducts[bread] == buyingProductCount);
        }
    }
}