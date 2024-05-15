using System.Runtime.Serialization;

namespace Isu.Tools
{
    [Serializable]
    public class StudentNotExistsException : Exception
    {
        public StudentNotExistsException() { }

        public StudentNotExistsException(string message) : base(message) { }

        public StudentNotExistsException(string message, Exception inner) : base(message, inner) { }

        protected StudentNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}