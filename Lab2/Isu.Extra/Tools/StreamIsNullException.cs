using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class StreamIsNullException : Exception
{
    public StreamIsNullException() { }

    public StreamIsNullException(string message)
        : base(message) { }

    public StreamIsNullException(string message, Exception inner)
        : base(message, inner) { }

    protected StreamIsNullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
