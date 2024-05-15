using System.Runtime.Serialization;

namespace Isu.Tools
{
    [Serializable]
    public class GroupAlreadyExistsException : Exception
    {
        public GroupAlreadyExistsException() { }

        public GroupAlreadyExistsException(string message) : base(message) { }

        public GroupAlreadyExistsException(string message, Exception inner) : base(message, inner) { }

        protected GroupAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    
    
    
    
    
}
