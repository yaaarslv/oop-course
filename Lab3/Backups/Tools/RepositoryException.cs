namespace Backups.Tools;

public class RepositoryException : Exception
{
    private RepositoryException(string message)
        : base(message) { }

    public static RepositoryException PathIsNullException()
    {
        return new RepositoryException("Path is null!");
    }

    public static RepositoryException RepositoryIsNullException()
    {
        return new RepositoryException("Repository is null!");
    }
}