using Isu.Extra.Tools;
using Isu.Tools;

namespace Isu.Extra.Entities;

public class Stream
{
    public const int MaxStreamCapacity = 20;
    private const int MondayIndex = 1;
    private const int SundayIndex = 7;
    private readonly List<IsuExtraStudent> _students;

    public Stream()
    {
        _students = new List<IsuExtraStudent>();
        Schedule = new Schedule();
    }

    public IReadOnlyCollection<IsuExtraStudent> Students => _students;
    public Schedule Schedule { get; }
    public IsuExtraStudent AddStudent(IsuExtraStudent student)
    {
        if (student is null)
        {
            throw new StudentIsNullException("Student is null!");
        }

        if (_students.Contains(student))
        {
            throw new StudentAlreadyExistsException($"Student {student.Id} already exists in stream!");
        }

        CheckStreamCapacity();
        CheckLessonsIntersection(student);
        _students.Add(student);
        return student;
    }

    public void RemoveStudent(IsuExtraStudent student)
    {
        if (student is null)
        {
            throw new StudentIsNullException("Student is null!");
        }

        if (!_students.Contains(student))
        {
            throw new StudentNotExistsException($"Student {student.Id} doesn't exist in stream!");
        }

        _students.Remove(student);
    }

    private void CheckStreamCapacity()
    {
        if (_students.Count >= MaxStreamCapacity)
        {
            throw new StreamIsFullException($"Can't add student to a stream because stream is full!");
        }
    }

    private bool CheckLessonsIntersectionOnDay(IsuExtraStudent student, int day)
    {
        return Schedule.Lessons[day].Any(streamLesson => student.Group.Schedule.Lessons[day].Any(groupLesson => streamLesson.StartTime == groupLesson.StartTime && streamLesson.EndTime == groupLesson.EndTime));
    }

    private void CheckLessonsIntersection(IsuExtraStudent student)
    {
        for (int i = MondayIndex; i <= SundayIndex; i++)
        {
            if (CheckLessonsIntersectionOnDay(student, i))
            {
                throw new LessonIntersectionException("Group lessons have intersection with stream lessons!");
            }
        }
    }
}