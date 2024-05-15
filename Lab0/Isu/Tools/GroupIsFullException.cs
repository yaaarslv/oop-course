using System.Runtime.Serialization;

namespace Isu.Tools
{
    [Serializable]
    public class GroupIsFullException : Exception
    {
        public GroupIsFullException() { }

        public GroupIsFullException(string message) : base(message) { }

        public GroupIsFullException(string message, Exception inner) : base(message, inner) { }

        protected GroupIsFullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}