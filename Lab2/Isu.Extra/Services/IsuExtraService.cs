using Isu.Extra.Entities;
using Isu.Extra.Tools;
using Isu.Tools;
using Stream = Isu.Extra.Entities.Stream;

namespace Isu.Extra.Services;

public class IsuExtraService
{
    private readonly List<Ognp> _ognps;
    private readonly List<IsuExtraGroup> _groups;

    public IsuExtraService()
    {
        _ognps = new List<Ognp>();
        _groups = new List<IsuExtraGroup>();
    }

    public IReadOnlyCollection<Ognp> Ognps => _ognps;
    public IReadOnlyCollection<IsuExtraGroup> Groups => _groups;

    public IsuExtraGroup AddGroup(string name)
    {
        if (_groups.Any(group => group.GroupName == name))
        {
            throw new GroupAlreadyExistsException($"The group {name} is already created!");
        }

        IsuExtraGroup newGroup = new IsuExtraGroup(name);
        _groups.Add(newGroup);
        return newGroup;
    }

    public IsuExtraStudent AddStudent(IsuExtraGroup group, string name)
    {
        IsuExtraStudent newStudent = new IsuExtraStudent(name, group);
        group.AddStudent(newStudent);
        newStudent.SetGroup(group);
        return newStudent;
    }

    public Ognp AddOgnp(string name, string faculty)
    {
        if (_ognps.Any(ognp => ognp.Name == name))
        {
            throw new OgnpAlreadyExistsException($"OGNP {name} is already created!");
        }

        Ognp ognp = new Ognp(name, faculty);
        _ognps.Add(ognp);
        return ognp;
    }

    public Stream AddStream(Ognp ognp)
    {
        if (ognp is null)
        {
            throw new OgnpIsNullException("OGNP is null!");
        }

        Stream stream = new Stream();
        ognp.AddStream(stream);
        return stream;
    }

    public Stream AddStudentToOgnp(IsuExtraStudent student, Ognp ognp)
    {
        if (student is null)
        {
            throw new StudentIsNullException("Student  is null!");
        }

        if (ognp is null)
        {
            throw new OgnpIsNullException("OGNP is null!");
        }

        Stream stream = ognp.AddToStream(student);
        return stream;
    }

    public void RemoveStudentFromOgnp(IsuExtraStudent student, Ognp ognp)
    {
        if (student is null)
        {
            throw new StudentIsNullException("Student  is null!");
        }

        if (ognp is null)
        {
            throw new OgnpIsNullException("OGNP is null!");
        }

        ognp.RemoveFromStream(student);
    }

    public IReadOnlyCollection<Stream> FindStreams(Ognp ognp)
    {
        if (ognp is null)
        {
            throw new OgnpIsNullException("OGNP is null!");
        }

        var streams = _ognps.Where(o => o == ognp).SelectMany(o => o.Streams);
        return streams.ToList();
    }

    public IReadOnlyCollection<IsuExtraStudent>? FindStudents(Stream stream)
    {
        if (stream is null)
        {
            throw new StreamIsNullException("Stream is null!");
        }

        var students = _ognps.SelectMany(o => o.Streams).SingleOrDefault(s => s == stream)?.Students;
        return students;
    }

    public IReadOnlyCollection<IsuExtraStudent> FindStudents(IsuExtraGroup group)
    {
        if (group is null)
        {
            throw new GroupIsNullException("Group is null!");
        }

        var students = group.ExtraStudents.Where(s => s.Ognps.Count == 0);
        return students.ToList();
    }

    public void ChangeOgnp(IsuExtraStudent student, Ognp oldOgnp, Ognp newOgnp)
    {
        if (student is null)
        {
            throw new StudentIsNullException("Student is null!");
        }

        if (newOgnp is null)
        {
            throw new OgnpIsNullException("Ognp is null!");
        }

        oldOgnp.RemoveFromStream(student);
        newOgnp.AddToStream(student);
    }

    public void ChangeStudentGroup(IsuExtraStudent student, IsuExtraGroup newGroup)
    {
        if (student is null)
        {
            throw new StudentIsNullException("Student is null!");
        }

        if (newGroup is null)
        {
            throw new GroupIsNullException("New group is null!");
        }

        IsuExtraGroup oldGroup = student.Group;
        newGroup.AddStudent(student);
        student.SetGroup(newGroup);
        oldGroup.RemoveStudent(student);
    }
}