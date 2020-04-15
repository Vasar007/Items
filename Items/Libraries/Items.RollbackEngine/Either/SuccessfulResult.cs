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
#pragma warning disable CS8601 // Possible null reference assignment.
            Result = result;
#pragma warning restore CS8601 // Possible null reference assignment.
            RollbackList = rollbackList.ThrowIfNull(nameof(rollbackList));
        }
    }
}
