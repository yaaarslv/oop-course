using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class LessonIntersectionException : Exception
{
    public LessonIntersectionException() { }

    public LessonIntersectionException(string message)
        : base(message) { }

    public LessonIntersectionException(string message, Exception inner)
        : base(message, inner) { }

    protected LessonIntersectionException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
