using Isu.Entities;
using Isu.Tools;

namespace Isu.Extra.Entities;

public class IsuExtraGroup : Group
{
    private readonly List<IsuExtraStudent> _extraStudents;

    public IsuExtraGroup(string name)
        : base(name)
    {
        _extraStudents = new List<IsuExtraStudent>();
        Schedule = new Schedule();
    }

    public IReadOnlyCollection<IsuExtraStudent> ExtraStudents => _extraStudents;
    public Schedule Schedule { get; }

    internal void AddStudent(IsuExtraStudent student)
    {
        CheckGroupCapacity();

        if (CheckStudentExistence(student))
        {
            throw new StudentAlreadyExistsException($"Can't add student to a group {GroupName} because he is already exists in group {student.Group.GroupName}");
        }

        _extraStudents.Add(student);
    }

    internal void RemoveStudent(IsuExtraStudent student)
    {
        if (!CheckStudentExistence(student))
        {
            throw new StudentNotExistsException($"Can't pop student because he doesn't exist in group {GroupName}");
        }

        _extraStudents.Remove(student);
    }

    private bool CheckStudentExistence(IsuExtraStudent student)
    {
        return _extraStudents.Any(s => s == student);
    }
}