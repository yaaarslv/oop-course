using System.Runtime.Serialization;

namespace Shops.Tools
{
    [Serializable]
    public class ZeroProductsException : Exception
    {
        public ZeroProductsException() { }

        public ZeroProductsException(string message) : base(message) { }

        public ZeroProductsException(string message, Exception inner) : base(message, inner) { }

        protected ZeroProductsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}