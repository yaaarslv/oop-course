using System.Runtime.Serialization;

namespace Shops.Tools
{
    [Serializable]
    public class PriceIsZeroException : Exception
    {
        public PriceIsZeroException() { }

        public PriceIsZeroException(string message) : base(message) { }

        public PriceIsZeroException(string message, Exception inner) : base(message, inner) { }

        protected PriceIsZeroException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}