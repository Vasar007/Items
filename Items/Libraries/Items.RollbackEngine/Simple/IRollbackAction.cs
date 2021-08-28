namespace Items.RollbackEngine.Simple
{
    public interface IRollbackAction
    {
        bool TryRollback();
    }
}
