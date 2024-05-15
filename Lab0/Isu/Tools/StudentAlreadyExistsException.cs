using System.Runtime.Serialization;

namespace Isu.Tools
{
    [Serializable]
    public class StudentAlreadyExistsException : Exception
    {
        public StudentAlreadyExistsException() { }

        public StudentAlreadyExistsException(string message) : base(message) { }

        public StudentAlreadyExistsException(string message, Exception inner) : base(message, inner) { }

        protected StudentAlreadyExistsException(SerializationInfo info, StreamingContext context) :
            base(info, context) { }
    }
}