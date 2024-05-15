using System.Runtime.Serialization;

namespace Shops.Tools
{
    [Serializable]
    public class ProductIsNullException : Exception
    {
        public ProductIsNullException() { }

        public ProductIsNullException(string message) : base(message) { }

        public ProductIsNullException(string message, Exception inner) : base(message, inner) { }

        protected ProductIsNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}