using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class InvalidDayException : Exception
{
    public InvalidDayException() { }

    public InvalidDayException(string message)
        : base(message) { }

    public InvalidDayException(string message, Exception inner)
        : base(message, inner) { }

    protected InvalidDayException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
