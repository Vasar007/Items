using System;

namespace Items.RollbackEngine.Saga
{
    internal abstract class ActivityHost
    {
        private readonly Action<Uri?, RoutingSlip> _send;

        protected ActivityHost(Action<Uri?, RoutingSlip> send)
        {
            _send = send;
        }

        public void ProcessForwardMessage(RoutingSlip routingSlip)
        {
            if (!routingSlip.IsCompleted)
            {
                // if the current step is successful, proceed
                // otherwise go to the Unwind path
                if (routingSlip.ProcessNext())
                {
                    // recursion stands for passing context via message
                    // the routing slip can be fully serialized and passed
                    // between systems. 
                    _send(routingSlip.ProgressUri, routingSlip);
                }
                else
                {
                    // pass message to unwind message route
                    _send(routingSlip.CompensationUri, routingSlip);
                }
            }
        }

        public void ProcessBackwardMessage(RoutingSlip routingSlip)
        {
            if (routingSlip.IsInProgress)
            {
                // UndoLast can put new work on the routing slip
                // and return false to go back on the forward 
                // path.
                if (routingSlip.UndoLast())
                {
                    // recursion stands for passing context via message
                    // the routing slip can be fully serialized and passed
                    // between systems .
                    _send(routingSlip.CompensationUri, routingSlip);
                }
                else
                {
                    _send(routingSlip.ProgressUri, routingSlip);
                }
            }
        }

        public abstract bool AcceptMessage(Uri? uri, RoutingSlip routingSlip);
    }
}
