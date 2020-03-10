using System;

namespace Items.RollbackEngine.Either
{
    public interface IRollbackAction
    {
        Boolean TryRollbackSafe();
    }
    public interface IRollbackAction<TIn, TOut> : IRollbackAction
    {
        TOut Execute(TIn parameter);
    }
}
