using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class FacultyIsNullException : Exception
{
    public FacultyIsNullException() { }

    public FacultyIsNullException(string message)
        : base(message) { }

    public FacultyIsNullException(string message, Exception inner)
        : base(message, inner) { }

    protected FacultyIsNullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
