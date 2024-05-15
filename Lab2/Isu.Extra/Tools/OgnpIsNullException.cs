using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class OgnpIsNullException : Exception
{
    public OgnpIsNullException() { }

    public OgnpIsNullException(string message)
        : base(message) { }

    public OgnpIsNullException(string message, Exception inner)
        : base(message, inner) { }

    protected OgnpIsNullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
