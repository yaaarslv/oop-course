namespace DataAccessLayer.Tools;

public class ReportException : Exception
{
    private ReportException(string message)
        : base(message) { }

    public static ReportException ReportIsNullException()
    {
        return new ReportException("Report is null!");
    }
}