using System.Runtime.Serialization;

namespace Isu.Extra.Tools;

public class LessonNotExistsException : Exception
{
    public LessonNotExistsException() { }

    public LessonNotExistsException(string message)
        : base(message) { }

    public LessonNotExistsException(string message, Exception inner)
        : base(message, inner) { }

    protected LessonNotExistsException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
