using System.Runtime.Serialization;

namespace Isu.Tools
{
    [Serializable]
    public class InvalidGroupNameException : Exception
    {
        public InvalidGroupNameException() { }

        public InvalidGroupNameException(string message) : base(message) { }

        public InvalidGroupNameException(string message, Exception inner) : base(message, inner) { }

        protected InvalidGroupNameException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}