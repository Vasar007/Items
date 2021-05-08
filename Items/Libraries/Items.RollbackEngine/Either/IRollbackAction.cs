using System.Diagnostics.CodeAnalysis;

namespace Items.RollbackEngine.Either
{
    public interface IRollbackAction
    {
        bool TryRollbackSafe();
    }

    public interface IRollbackAction<TIn, TOut> : IRollbackAction
    {
        [return: MaybeNull]
        TOut Execute([AllowNull] TIn parameter);
    }
}
