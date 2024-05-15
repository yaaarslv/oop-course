using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class StudentIsNullException : Exception
{
    public StudentIsNullException() { }

    public StudentIsNullException(string message)
        : base(message) { }

    public StudentIsNullException(string message, Exception inner)
        : base(message, inner) { }

    protected StudentIsNullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
