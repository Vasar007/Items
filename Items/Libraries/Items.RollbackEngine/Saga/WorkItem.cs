using System;
using Acolyte.Assertions;

namespace Items.RollbackEngine.Saga
{
    internal abstract class WorkItem
    {
        public RoutingSlip? RoutingSlip { get; set; }

        public WorkItemArguments Arguments { get; set; }

        public abstract Type ActivityType { get; }


        protected WorkItem(WorkItemArguments arguments)
        {
            Arguments = arguments.ThrowIfNull(nameof(arguments));
        }
    }
}
