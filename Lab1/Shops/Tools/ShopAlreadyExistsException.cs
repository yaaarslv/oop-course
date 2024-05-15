using System.Runtime.Serialization;

namespace Shops.Tools
{
    [Serializable]
    public class ShopAlreadyExistsException : Exception
    {
        public ShopAlreadyExistsException() { }

        public ShopAlreadyExistsException(string message) : base(message) { }

        public ShopAlreadyExistsException(string message, Exception inner) : base(message, inner) { }

        protected ShopAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}