using System.Runtime.Serialization;

namespace Shops.Tools
{
    [Serializable]
    public class TooManyBuyingProductsException : Exception
    {
        public TooManyBuyingProductsException() { }

        public TooManyBuyingProductsException(string message) : base(message) { }

        public TooManyBuyingProductsException(string message, Exception inner) : base(message, inner) { }

        protected TooManyBuyingProductsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}