using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class TeacherIsNullException : Exception
{
    public TeacherIsNullException() { }

    public TeacherIsNullException(string message)
        : base(message) { }

    public TeacherIsNullException(string message, Exception inner)
        : base(message, inner) { }

    protected TeacherIsNullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
