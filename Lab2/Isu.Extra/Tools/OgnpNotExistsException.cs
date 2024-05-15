using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class OgnpNotExistsException : Exception
{
    public OgnpNotExistsException() { }

    public OgnpNotExistsException(string message)
        : base(message) { }

    public OgnpNotExistsException(string message, Exception inner)
        : base(message, inner) { }

    protected OgnpNotExistsException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
