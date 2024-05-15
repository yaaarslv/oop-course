using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class Schedule
{
    private const int MondayIndex = 1;
    private const int TuesdayIndex = 2;
    private const int WednesdayIndex = 3;
    private const int ThursdayIndex = 4;
    private const int FridayIndex = 5;
    private const int SaturdayIndex = 6;
    private const int SundayIndex = 7;
    private readonly Dictionary<int, List<Lesson>> _lessons;
    public Schedule()
    {
        _lessons = new Dictionary<int, List<Lesson>>()
            {
                { MondayIndex, new List<Lesson>() },
                { TuesdayIndex, new List<Lesson>() },
                { WednesdayIndex, new List<Lesson>() },
                { ThursdayIndex, new List<Lesson>() },
                { FridayIndex, new List<Lesson>() },
                { SaturdayIndex, new List<Lesson>() },
                { SundayIndex, new List<Lesson>() },
            };
    }

    public IReadOnlyDictionary<int, List<Lesson>> Lessons => _lessons;

    public Lesson AddLesson(int dayNumber, Lesson lesson)
    {
        if (!CheckDay(dayNumber))
        {
            throw new InvalidDayException("Day index is invalid!");
        }

        if (lesson is null)
        {
            throw new LessonIsNullException("Lesson is null!");
        }

        _lessons[dayNumber].Add(lesson);
        return lesson;
    }

    public void RemoveLesson(int dayNumber, Lesson lesson)
    {
        if (!CheckDay(dayNumber))
        {
            throw new InvalidDayException("Day index is invalid!");
        }

        if (lesson is null)
        {
            throw new LessonIsNullException("Lesson is null!");
        }

        if (!_lessons[dayNumber].Contains(lesson))
        {
            throw new LessonNotExistsException("This lesson doesn't exists in timetable!");
        }

        _lessons[dayNumber].Remove(lesson);
    }

    private bool CheckDay(int day)
    {
        return day >= MondayIndex && day <= SundayIndex;
    }
}