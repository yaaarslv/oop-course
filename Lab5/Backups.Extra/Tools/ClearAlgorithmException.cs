namespace Backups.Extra.Tools;

public class ClearAlgorithmException : Exception
{
    private ClearAlgorithmException(string message)
        : base(message) { }

    public static ClearAlgorithmException DeletingAllRestorePoints()
    {
        return new ClearAlgorithmException("You are trying to delete all restore points!");
    }

    public static ClearAlgorithmException ClearAlgorithmIsNull()
    {
        return new ClearAlgorithmException("Clear algorithm is null!");
    }
}