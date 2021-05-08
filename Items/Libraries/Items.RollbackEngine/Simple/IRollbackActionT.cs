namespace Items.RollbackEngine.Simple
{
    public interface IRollbackAction<in T>
    {
        bool TryRollbackSafe(T rollbackParameter);
    }
}
