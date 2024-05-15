using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class OgnpCountIsMaxException : Exception
{
    public OgnpCountIsMaxException() { }

    public OgnpCountIsMaxException(string message)
        : base(message) { }

    public OgnpCountIsMaxException(string message, Exception inner)
        : base(message, inner) { }

    protected OgnpCountIsMaxException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
