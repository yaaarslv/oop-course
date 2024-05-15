using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class StreamIsFullException : Exception
{
    public StreamIsFullException() { }

    public StreamIsFullException(string message)
        : base(message) { }

    public StreamIsFullException(string message, Exception inner)
        : base(message, inner) { }

    protected StreamIsFullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
