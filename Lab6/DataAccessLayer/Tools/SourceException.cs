namespace DataAccessLayer.Tools;

public class SourceException : Exception
{
    private SourceException(string message)
        : base(message) { }

    public static SourceException SourceIsNullException()
    {
        return new SourceException("Source is null!");
    }

    public static SourceException SourceInfoIsNullException()
    {
        return new SourceException("Source info is null!");
    }

    public static SourceException SourceAlreadyExistsException()
    {
        return new SourceException("Source already exists in list!");
    }
}