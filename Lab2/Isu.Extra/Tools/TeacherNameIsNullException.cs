using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class TeacherNameIsNullException : Exception
{
    public TeacherNameIsNullException() { }

    public TeacherNameIsNullException(string message)
        : base(message) { }

    public TeacherNameIsNullException(string message, Exception inner)
        : base(message, inner) { }

    protected TeacherNameIsNullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
