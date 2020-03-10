using System;

namespace Items.RollbackEngine.Saga
{
    internal class ActivityHost<T> : ActivityHost
        where T : Activity, new()
    {
        public ActivityHost(Action<Uri?, RoutingSlip> send)
            : base(send)
        {
        }

        public override Boolean AcceptMessage(Uri? uri, RoutingSlip routingSlip)
        {
            var activity = new T();
            if (activity.CompensationQueueAddress.Equals(uri))
            {
                ProcessBackwardMessage(routingSlip);
                return true;
            }
            if (activity.WorkItemQueueAddress.Equals(uri))
            {
                ProcessForwardMessage(routingSlip);
                return true;
            }
            return false;
        }
    }
}
