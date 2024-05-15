using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class GroupIsNullException : Exception
{
    public GroupIsNullException() { }

    public GroupIsNullException(string message)
        : base(message) { }

    public GroupIsNullException(string message, Exception inner)
        : base(message, inner) { }

    protected GroupIsNullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
