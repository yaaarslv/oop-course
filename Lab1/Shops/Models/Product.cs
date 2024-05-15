using Shops.Tools;

namespace Shops.Models
{
    public class Product
    {
        public string Name { get; }
        public decimal Price { get; }

        public Product(string name, decimal price)
        {
            if (name is null)
            {
                throw new NullReferenceException("Name of product is null!");
            }

            if (price == 0)
            {
                throw new PriceIsZeroException("Price of product is zero!");
            }

            Name = name;
            Price = price;
        }
    }
}