namespace Items.RollbackEngine.Simple
{
    public interface IRollbackAction<in T>
    {
        bool TryRollback(T rollbackParameter);
    }
}
