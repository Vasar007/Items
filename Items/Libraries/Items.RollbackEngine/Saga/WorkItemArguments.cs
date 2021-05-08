using System.Collections.Generic;

namespace Items.RollbackEngine.Saga
{
    internal sealed class WorkItemArguments : Dictionary<string, object>
    {
        public WorkItemArguments()
        {
        }
    }
}
