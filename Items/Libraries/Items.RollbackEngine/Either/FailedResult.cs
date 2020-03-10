using System;
using Acolyte.Assertions;

namespace Items.RollbackEngine.Either
{
    public sealed class FailedResult : IRollbackActionResult<Exception>
    {
        public Exception Exception { get; }


        public FailedResult(Exception exception)
        {
            Exception = exception.ThrowIfNull(nameof(exception));
        }
    }
}
