using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class OgnpNameIsNullException : Exception
{
    public OgnpNameIsNullException() { }

    public OgnpNameIsNullException(string message)
        : base(message) { }

    public OgnpNameIsNullException(string message, Exception inner)
        : base(message, inner) { }

    protected OgnpNameIsNullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
