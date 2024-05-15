using Isu.Entities;
using Isu.Extra.Tools;
using Isu.Models;

namespace Isu.Extra.Entities;

public class IsuExtraStudent
{
    private const int MaxOgnpCount = 2;
    private readonly List<Ognp> _ognps;

    public IsuExtraStudent(string name, IsuExtraGroup group)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new NullReferenceException("Name is null!");
        }

        if (group is null)
        {
            throw new NullReferenceException("Group is null!");
        }

        _ognps = new List<Ognp>();
        Id = int.Parse(GetHashCode().ToString()[..^2]);
        Name = name;
        Group = group;
        CourseNumber = Group.CourseNumber;
        Faculty = GetFaculty(group.GroupName[0]);
    }

    public IReadOnlyCollection<Ognp> Ognps => _ognps;
    public int Id { get; }
    public string Name { get; }
    public IsuExtraGroup Group { get; private set; }
    public CourseNumber? CourseNumber { get; }
    public string Faculty { get; }

    public void AddOgnp(Ognp ognp)
    {
        if (_ognps.Count == MaxOgnpCount)
        {
            throw new OgnpCountIsMaxException("Student has max OGNP count!");
        }

        if (CheckOgnpExistence(ognp))
        {
            throw new OgnpAlreadyExistsException($"Student {Id} already has OGNP {ognp.Name}!");
        }

        _ognps.Add(ognp);
    }

    public void RemoveOgnp(Ognp ognp)
    {
        if (!_ognps.Contains(ognp))
        {
            throw new OgnpNotExistsException($"Student {Id} does't have OGNP {ognp.Name}!");
        }

        _ognps.Remove(ognp);
    }

    public void SetGroup(IsuExtraGroup group)
    {
        if (group is null)
        {
            throw new GroupIsNullException("Group is null!");
        }

        Group = group;
    }

    private bool CheckOgnpExistence(Ognp ognp)
    {
        if (ognp is null)
        {
            throw new OgnpIsNullException("Ognp is null!");
        }

        return _ognps.Contains(ognp);
    }

    private string GetFaculty(char code)
    {
        return code switch
        {
            'L' => "ИЛТ",
            'D' => "МРиП",
            'O' => "НОЦ инфохимии",
            'N' => "ФБИТ",
            'T' => "ФБТ",
            'K' => "ФИКТ",
            'M' => "ФИТиП",
            'P' => "ФПИиКТ",
            'R' => "ФСУиР",
            'U' => "ФТМИ",
            'V' => "Ф фотоники",
            'G' => "ФЭТ",
            'Z' => "ФизФ",
            'H' => "Центр ХИ",
            'W' => "ФЭиЭТ",
            'B' => "ЦПО",
            _ => string.Empty
        };
    }
}