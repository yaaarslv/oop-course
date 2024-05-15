using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class SameOgnpFacultyException : Exception
{
    public SameOgnpFacultyException() { }

    public SameOgnpFacultyException(string message)
        : base(message) { }

    public SameOgnpFacultyException(string message, Exception inner)
        : base(message, inner) { }

    protected SameOgnpFacultyException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
