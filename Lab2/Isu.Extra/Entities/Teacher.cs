using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class Teacher
{
    public Teacher(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new TeacherNameIsNullException("Teacher name is null!");
        }

        Id = Guid.NewGuid();
        Name = name;
    }

    public Guid Id { get; }
    public string Name { get; }
}