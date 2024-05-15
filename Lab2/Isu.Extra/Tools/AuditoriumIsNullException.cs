using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class AuditoriumIsNullException : Exception
{
    public AuditoriumIsNullException() { }

    public AuditoriumIsNullException(string message)
        : base(message) { }

    public AuditoriumIsNullException(string message, Exception inner)
        : base(message, inner) { }

    protected AuditoriumIsNullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
