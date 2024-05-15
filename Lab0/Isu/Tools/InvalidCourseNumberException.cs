using System.Runtime.Serialization;

namespace Isu.Tools
{
    [Serializable]
    public class InvalidCourseNumberException : Exception
    {
        public InvalidCourseNumberException() { }

        public InvalidCourseNumberException(string message) : base(message) { }

        public InvalidCourseNumberException(string message, Exception inner) : base(message, inner) { }

        protected InvalidCourseNumberException(SerializationInfo info, StreamingContext context) :
            base(info, context) { }
    }
}