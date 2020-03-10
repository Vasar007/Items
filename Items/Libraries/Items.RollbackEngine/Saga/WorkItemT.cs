using System;

namespace Items.RollbackEngine.Saga
{
    internal class WorkItem<T> : WorkItem
        where T : Activity
    {
        public override Type ActivityType => typeof(T);


        public WorkItem(WorkItemArguments args)
            : base(args)
        {
        }
    }
}
