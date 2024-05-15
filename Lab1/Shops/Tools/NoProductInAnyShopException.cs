using System.Runtime.Serialization;

namespace Shops.Tools
{
    [Serializable]
    public class NoProductInAnyShopException : Exception
    {
        public NoProductInAnyShopException() { }

        public NoProductInAnyShopException(string message) : base(message) { }

        public NoProductInAnyShopException(string message, Exception inner) : base(message, inner) { }

        protected NoProductInAnyShopException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
