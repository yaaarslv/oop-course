using System.Runtime.Serialization;

namespace Shops.Tools
{
    [Serializable]
    public class ShopNameIsNullException : Exception
    {
        public ShopNameIsNullException() { }

        public ShopNameIsNullException(string message) : base(message) { }

        public ShopNameIsNullException(string message, Exception inner) : base(message, inner) { }

        protected ShopNameIsNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}