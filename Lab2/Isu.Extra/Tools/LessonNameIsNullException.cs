using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class LessonNameIsNullException : Exception
{
    public LessonNameIsNullException() { }

    public LessonNameIsNullException(string message)
        : base(message) { }

    public LessonNameIsNullException(string message, Exception inner)
        : base(message, inner) { }

    protected LessonNameIsNullException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}