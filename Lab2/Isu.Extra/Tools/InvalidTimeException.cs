using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class InvalidTimeException : Exception
{
    public InvalidTimeException() { }

    public InvalidTimeException(string message)
        : base(message) { }

    public InvalidTimeException(string message, Exception inner)
        : base(message, inner) { }

    protected InvalidTimeException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
