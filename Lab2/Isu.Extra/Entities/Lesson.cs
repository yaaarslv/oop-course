using Isu.Extra.Models;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class Lesson
{
    private const int MinHourValue = 0;
    private const int MaxHourValue = 23;
    private const int MinMinutesValue = 0;
    private const int MaxMinutesValue = 59;
    public Lesson(string name, Teacher teacher, IsuExtraGroup group, Auditorium auditorium, string startTime, string endTime)
    {
        if (name is null)
        {
            throw new LessonNameIsNullException("Lesson name is null!");
        }

        if (teacher is null)
        {
            throw new TeacherIsNullException("Teacher is null!");
        }

        if (group is null)
        {
            throw new GroupIsNullException("Group is null!");
        }

        if (auditorium is null)
        {
            throw new AuditoriumIsNullException("Auditorium is null!");
        }

        if (startTime is null)
        {
            throw new TimeIsNullException("Lesson start time is null!");
        }

        CheckLessonTime(startTime);

        if (endTime is null)
        {
            throw new TimeIsNullException("Lesson end time is null!");
        }

        CheckLessonTime(endTime);

        Name = name;
        Teacher = teacher;
        Group = group;
        Auditorium = auditorium;
        StartTime = startTime;
        EndTime = endTime;
    }

    public string Name { get; }
    public Teacher Teacher { get; }
    public IsuExtraGroup Group { get; }
    public Auditorium Auditorium { get; }
    public string StartTime { get; }
    public string EndTime { get; }

    private void CheckLessonTime(string time)
    {
        if (time is null)
        {
            throw new TimeIsNullException("Time is null!");
        }

        int hours = int.Parse(time.Split(":")[0]);
        int minutes = int.Parse(time.Split(":")[1]);
        if (hours < MinHourValue || hours > MaxHourValue || minutes < MinMinutesValue || minutes > MaxMinutesValue)
        {
            throw new InvalidTimeException("Time is invalid!");
        }
    }
}