using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class TimeIsNullException : Exception
{
    public TimeIsNullException() { }

    public TimeIsNullException(string message)
        : base(message) { }

    public TimeIsNullException(string message, Exception inner)
        : base(message, inner) { }

    protected TimeIsNullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
