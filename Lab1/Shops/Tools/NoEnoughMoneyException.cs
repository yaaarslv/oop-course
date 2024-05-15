using System.Runtime.Serialization;

namespace Shops.Tools
{
    [Serializable]
    public class NoEnoughMoneyException : Exception
    {
        public NoEnoughMoneyException() { }

        public NoEnoughMoneyException(string message) : base(message) { }

        public NoEnoughMoneyException(string message, Exception inner) : base(message, inner) { }

        protected NoEnoughMoneyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}