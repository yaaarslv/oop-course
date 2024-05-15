using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class OgnpAlreadyExistsException : Exception
{
    public OgnpAlreadyExistsException() { }

    public OgnpAlreadyExistsException(string message)
        : base(message) { }

    public OgnpAlreadyExistsException(string message, Exception inner)
        : base(message, inner) { }

    protected OgnpAlreadyExistsException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
