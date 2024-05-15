using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class Ognp
{
    private const int MaxStreamsCount = 3;
    private readonly List<Stream> _streams;

    public Ognp(string name, string faculty)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new OgnpNameIsNullException("OGNP name is null!");
        }

        if (string.IsNullOrWhiteSpace(faculty))
        {
            throw new FacultyIsNullException("Faculty is null!");
        }

        _streams = new List<Stream>();
        Name = name;
        Faculty = faculty;
    }

    public string Name { get; }
    public string Faculty { get; }
    public IReadOnlyCollection<Stream> Streams => _streams;

    public Stream AddStream(Stream stream)
    {
        if (stream is null)
        {
            throw new StreamIsNullException("Stream is null!");
        }

        CheckOgnpCapacity();

        _streams.Add(stream);
        return stream;
    }

    public Stream AddToStream(IsuExtraStudent student)
    {
        if (student is null)
        {
            throw new StudentIsNullException("Student is null!");
        }

        if (Faculty == student.Faculty)
        {
            throw new SameOgnpFacultyException("Trying to add student to OGNP with his faculty!");
        }

        var stream = _streams.FirstOrDefault(s => s.Students.Count < Stream.MaxStreamCapacity);
        if (stream is null)
        {
            throw new StreamIsNullException("All streams are full!");
        }

        stream.AddStudent(student);
        student.AddOgnp(this);
        return stream;
    }

    public void RemoveFromStream(IsuExtraStudent student)
    {
        if (student is null)
        {
            throw new StudentIsNullException("Student is null!");
        }

        var stream = _streams.SingleOrDefault(s => s.Students.Contains(student));
        if (stream is null)
        {
            throw new StreamIsNullException("Any streams don't have this student!");
        }

        stream.RemoveStudent(student);
        student.RemoveOgnp(this);
    }

    private void CheckOgnpCapacity()
    {
        if (_streams.Count >= MaxStreamsCount)
        {
            throw new OgnpIsFullException($"Can't add stream to a OGNP {Name} because OGNP is full!");
        }
    }
}