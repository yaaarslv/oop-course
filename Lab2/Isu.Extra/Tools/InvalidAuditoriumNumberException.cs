using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class InvalidAuditoriumNumberException : Exception
{
    public InvalidAuditoriumNumberException() { }

    public InvalidAuditoriumNumberException(string message)
        : base(message) { }

    public InvalidAuditoriumNumberException(string message, Exception inner)
        : base(message, inner) { }

    protected InvalidAuditoriumNumberException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
