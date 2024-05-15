using Banks.Tools;

namespace Banks.Models;

public class Passport
{
    private const int PassportComponentsCount = 4;
    public Passport(string passportData)
    {
        if (string.IsNullOrWhiteSpace(passportData))
        {
            throw PassportException.PassportDataIsNullException();
        }

        var data = passportData.Split(", ");
        if (data.Length < PassportComponentsCount)
        {
            throw PassportException.PassportDataIsNotFullException();
        }

        Series = int.Parse(data[0]);
        Number = int.Parse(data[1]);
        IssuedBy = data[2];
        IssueDate = DateOnly.Parse(data[3]);
    }

    public int Series { get; }
    public int Number { get; }
    public string IssuedBy { get; }
    public DateOnly IssueDate { get; }

    public string StringPassportData()
    {
        return $"{Series}, {Number}, {IssuedBy}, {IssueDate}";
    }
}