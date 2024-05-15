using System.Runtime.Serialization;

namespace Shops.Tools
{
    [Serializable]
    public class ProductNotExistsException : Exception
    {
        public ProductNotExistsException() { }

        public ProductNotExistsException(string message) : base(message) { }

        public ProductNotExistsException(string message, Exception inner) : base(message, inner) { }

        protected ProductNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}