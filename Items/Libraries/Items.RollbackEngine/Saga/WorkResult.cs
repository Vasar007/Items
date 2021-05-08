using System.Collections.Generic;

namespace Items.RollbackEngine.Saga
{
    internal sealed class WorkResult : Dictionary<string, object>
    {
        public WorkResult()
        {
        }
    }
}
