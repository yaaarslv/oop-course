using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class OgnpIsFullException : Exception
{
    public OgnpIsFullException() { }

    public OgnpIsFullException(string message)
        : base(message) { }

    public OgnpIsFullException(string message, Exception inner)
        : base(message, inner) { }

    protected OgnpIsFullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
