using System;
using System.Collections.Generic;
using Items.Common.Utils;

namespace Items.RollbackEngine.Saga
{
    internal class RoutingSlip
    {
        private static readonly PrefixLogger Logger = PrefixLogger.Create(nameof(RoutingSlip));

        private readonly Stack<WorkLog> _completedWorkLogs = new Stack<WorkLog>();
        private readonly Queue<WorkItem> _nextWorkItem = new Queue<WorkItem>();

        public bool IsCompleted => _nextWorkItem.Count == 0;
        public bool IsInProgress => _completedWorkLogs.Count > 0;

        public Uri? ProgressUri
        {
            get
            {
                if (IsCompleted)
                {
                    return null;
                }
                else
                {
                    return
                        ((Activity) Activator.CreateInstance(_nextWorkItem.Peek().ActivityType)).
                        WorkItemQueueAddress;
                }
            }
        }

        public Uri? CompensationUri
        {
            get
            {
                if (!IsInProgress)
                {
                    return null;
                }
                else
                {
                    return
                        ((Activity) Activator.CreateInstance(_completedWorkLogs.Peek().ActivityType)).
                        CompensationQueueAddress;
                }
            }
        }


        public RoutingSlip()
        {
        }

        public RoutingSlip(IEnumerable<WorkItem> workItems)
        {
            foreach (WorkItem workItem in workItems)
            {
                _nextWorkItem.Enqueue(workItem);
            }
        }

        public bool ProcessNext()
        {
            if (IsCompleted)
            {
                throw new InvalidOperationException();
            }

            WorkItem currentItem = _nextWorkItem.Dequeue();
            var activity = (Activity) Activator.CreateInstance(currentItem.ActivityType);
            try
            {
                WorkLog? result = activity.DoWork(currentItem);
                if (result != null)
                {
                    _completedWorkLogs.Push(result);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, "Exception occurred during process next method.");
            }
            return false;
        }

        public bool UndoLast()
        {
            if (!IsInProgress)
            {
                throw new InvalidOperationException();
            }

            WorkLog currentItem = _completedWorkLogs.Pop();
            var activity = (Activity) Activator.CreateInstance(currentItem.ActivityType);
            try
            {
                return activity.Compensate(currentItem, this);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, "Exception occurred during undo last method.");
                throw;
            }

        }
    }
}
