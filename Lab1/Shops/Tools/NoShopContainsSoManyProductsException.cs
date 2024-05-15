using System.Runtime.Serialization;

namespace Shops.Tools
{
    [Serializable]
    public class NoShopContainsSoManyProducts : Exception
    {
        public NoShopContainsSoManyProducts() { }

        public NoShopContainsSoManyProducts(string message) : base(message) { }

        public NoShopContainsSoManyProducts(string message, Exception inner) : base(message, inner) { }

        protected NoShopContainsSoManyProducts(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}