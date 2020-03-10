using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Acolyte.Assertions;

namespace Items.RollbackEngine.Either
{
    public sealed class SuccessfulResult<TResult> : IRollbackActionResult<TResult>
    {
        [MaybeNull]
        public TResult Result { get; }

        public IEnumerable<IRollbackAction> RollbackList { get; }


        public SuccessfulResult([AllowNull] TResult result, IEnumerable<IRollbackAction> rollbackList)
        {
            Result = result;
            RollbackList = rollbackList.ThrowIfNull(nameof(rollbackList));
        }
    }
}
