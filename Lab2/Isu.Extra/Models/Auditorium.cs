using Isu.Extra.Tools;

namespace Isu.Extra.Models;

public class Auditorium
{
    private const int AuditoriumNumberLength = 4;

    public Auditorium(int number)
    {
        if (!CheckAuditoriumNumber(number))
        {
            throw new InvalidAuditoriumNumberException($"Auditorium number {number} is invalid!");
        }

        Number = number;
    }

    public int Number { get; }

    private bool CheckAuditoriumNumber(int number)
    {
        return number.ToString().Length == AuditoriumNumberLength;
    }
}