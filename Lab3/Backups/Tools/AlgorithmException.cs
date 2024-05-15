namespace Backups.Tools;

public class AlgorithmException : Exception
{
    private AlgorithmException(string message)
        : base(message) { }

    public static AlgorithmException AlgorithmIsNullException()
    {
        return new AlgorithmException("Algorithm is null!");
    }
}