using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Extra.Tools;
using Xunit;
using Stream = Isu.Extra.Entities.Stream;

namespace Isu.Extra.Test;

public class IsuExtraServiceTest
{
    private IsuExtraService _service = new IsuExtraService();

    [Fact]
    public void AddNewOgnpCourse()
    {
        Ognp ognp = _service.AddOgnp("Основы кибербезопасности", "ФБИТ");
        Assert.Contains(ognp, _service.Ognps);
    }

    [Fact]
    public void AddStudentToOgnp()
    {
        IsuExtraGroup m32051 = _service.AddGroup("M32051");
        IsuExtraStudent testStudent = _service.AddStudent(m32051, "Тестов Тест Тестович");
        Ognp ognp = _service.AddOgnp("Основы кибербезопасности", "ФБИТ");
        Stream stream = _service.AddStream(ognp);
        _service.AddStudentToOgnp(testStudent, ognp);
        Assert.Contains(ognp, testStudent.Ognps);
        Assert.Contains(testStudent, stream.Students);
    }

    [Fact]
    public void AddStudentToOgnp_SameFaculty_ThrowException()
    {
        Assert.Throws<SameOgnpFacultyException>(() =>
        {
            IsuExtraGroup n32451 = _service.AddGroup("N32451");
            IsuExtraStudent testStudent = _service.AddStudent(n32451, "Тестов Тест Тестович");
            Ognp ognp = _service.AddOgnp("Основы кибербезопасности", "ФБИТ");
            Stream stream = _service.AddStudentToOgnp(testStudent, ognp);
        });
    }

    [Fact]
    public void AddStudentToOgnp_MaxStudentsPerStreams_ThrowException()
    {
        Assert.Throws<StreamIsNullException>(() =>
        {
            IsuExtraGroup m32051 = _service.AddGroup("M32051");
            Ognp ognp = _service.AddOgnp("Основы кибербезопасности", "ФБИТ");
            Stream stream = _service.AddStream(ognp);
            for (int i = 0; i < (Stream.MaxStreamCapacity * ognp.Streams.Count) + 1; i++)
            {
                _service.AddStudentToOgnp(new IsuExtraStudent("Тестов Тест Тестович", m32051), ognp);
            }
        });
    }

    [Fact]
    public void AddStudentToOgnp_LessonEntersection_ThrowException()
    {
        IsuExtraGroup m32051 = _service.AddGroup("M32051");
        IsuExtraStudent testStudent = _service.AddStudent(m32051, "Василий Петрович Голобородько");
        m32051.Schedule.AddLesson(
            1, new Lesson("English", new Teacher("Тестов Тест Тестович"), m32051, new Auditorium(1557), "8:20", "9:50"));
        Ognp ognp = _service.AddOgnp("Основы кибербезопасности", "ФБИТ");
        Stream stream = _service.AddStream(ognp);
        stream.Schedule.AddLesson(
            1, new Lesson("NotEnglishButVeryImportantLesson", new Teacher("Нетестов Нетест Нетестович"), m32051, new Auditorium(1474), "8:20", "9:50"));

        Assert.Throws<LessonIntersectionException>(() =>
        {
            ognp.AddToStream(testStudent);
        });
    }

    [Fact]
    public void RemoveStudentFromOgnp()
    {
        IsuExtraGroup m32051 = _service.AddGroup("M32051");
        IsuExtraStudent testStudent = _service.AddStudent(m32051, "Василий Петрович Голобородько");
        Ognp ognp = _service.AddOgnp("Основы кибербезопасности", "ФБИТ");
        Stream stream = _service.AddStream(ognp);
        ognp.AddToStream(testStudent);
        _service.RemoveStudentFromOgnp(testStudent, ognp);
        Assert.DoesNotContain(testStudent, stream.Students);
        Assert.DoesNotContain(ognp, testStudent.Ognps);
    }

    [Fact]
    public void RemoveStudentFromOgnp_StudentNotExists_ThrowException()
    {
        IsuExtraGroup m32051 = _service.AddGroup("M32051");
        IsuExtraStudent testStudent = _service.AddStudent(m32051, "Василий Петрович Голобородько");
        Ognp ognp = _service.AddOgnp("Основы кибербезопасности", "ФБИТ");

        Assert.Throws<StreamIsNullException>(() =>
        {
            _service.RemoveStudentFromOgnp(testStudent, ognp);
        });
    }

    [Fact]
    public void GetStreamsInCourse()
    {
        Ognp ognp = _service.AddOgnp("Основы кибербезопасности", "ФБИТ");
        Stream stream1 = _service.AddStream(ognp);
        Stream stream2 = _service.AddStream(ognp);
        Stream stream3 = _service.AddStream(ognp);
        var streams = _service.FindStreams(ognp);
        foreach (var stream in streams)
        {
            Assert.Contains(stream, ognp.Streams);
        }

        Assert.True(streams.Count == ognp.Streams.Count);
    }

    [Fact]
    public void GetStudentsInStream()
    {
        IsuExtraGroup m32051 = _service.AddGroup("M32051");
        Ognp ognp = _service.AddOgnp("Основы кибербезопасности", "ФБИТ");
        Stream stream = _service.AddStream(ognp);
        IsuExtraStudent student1 = _service.AddStudent(m32051, "Тестов Тест Тестович");
        IsuExtraStudent student2 = _service.AddStudent(m32051, "Нетестов Нетест Нетестович");
        IsuExtraStudent student3 = _service.AddStudent(m32051, "Василий Петрович Голобородько");
        _service.AddStudentToOgnp(student1, ognp);
        _service.AddStudentToOgnp(student2, ognp);
        _service.AddStudentToOgnp(student3, ognp);
        var students = _service.FindStudents(stream);
        if (students is not null)
        {
            foreach (var student in students)
            {
                Assert.Contains(student, stream.Students);
            }

            Assert.True(students.Count == stream.Students.Count);
        }
    }

    [Fact]
    public void GetStudentsInGroupNotHaveOgnp()
    {
        IsuExtraGroup m32051 = _service.AddGroup("M32051");
        Ognp ognp = _service.AddOgnp("Основы кибербезопасности", "ФБИТ");
        Stream stream = _service.AddStream(ognp);
        IsuExtraStudent student1 = _service.AddStudent(m32051, "Тестов Тест Тестович");
        IsuExtraStudent student2 = _service.AddStudent(m32051, "Нетестов Нетест Нетестович");
        IsuExtraStudent student3 = _service.AddStudent(m32051, "Василий Петрович Голобородько");
        _service.AddStudentToOgnp(student1, ognp);
        var students = _service.FindStudents(m32051);
        foreach (var student in students)
        {
            Assert.True(student.Ognps.Count == 0);
        }
    }
}