using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class LessonIsNullException : Exception
{
    public LessonIsNullException() { }

    public LessonIsNullException(string message)
        : base(message) { }

    public LessonIsNullException(string message, Exception inner)
        : base(message, inner) { }

    protected LessonIsNullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
