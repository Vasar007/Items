using System;

namespace Items.RollbackEngine.Saga
{
    internal abstract class Activity
    {
        public abstract Uri WorkItemQueueAddress { get; }
        public abstract Uri CompensationQueueAddress { get; }

        protected Activity()
        {
        }

        public abstract WorkLog? DoWork(WorkItem item);

        public abstract bool Compensate(WorkLog item, RoutingSlip routingSlip);
    }
}
