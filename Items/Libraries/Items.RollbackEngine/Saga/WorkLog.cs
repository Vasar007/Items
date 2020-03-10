using System;
using Acolyte.Assertions;

namespace Items.RollbackEngine.Saga
{
    internal sealed class WorkLog
    {
        public WorkResult Result { get; }

        public Type ActivityType { get; }


        public WorkLog(Activity activity, WorkResult result)
        {
            Result = result.ThrowIfNull(nameof(result));
            ActivityType = activity.GetType().ThrowIfNull(nameof(activity));
        }
    }
}
