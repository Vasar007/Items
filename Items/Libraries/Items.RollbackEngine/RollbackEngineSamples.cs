using System;
using Items.Common;
using Items.RollbackEngine.Either;
using Items.RollbackEngine.Saga;

namespace Items.RollbackEngine
{
    public sealed class RollbackEngineSamples : ISamplesModule
    {
        public string ModuleName { get; } = nameof(RollbackEngineSamples);


        public RollbackEngineSamples()
        {
        }

        #region ISamplesModule Implementation

        public SampleCollection ProvideSamples()
        {
            return new SampleCollection
            {
                { "RollbackEngine.TaskEngine", RunTaskEngineSample },
                { "RollbackEngine.Either", RunEitherMonadSample },
                { "RollbackEngine.Saga", RunSagaEngineSample }
            };
        }

        #endregion

        #region TaskEngine

        public static void RunTaskEngineSample()
        {
            var te = new TaskEngine.TaskEngine();
            te.Demo();
        }

        #endregion

        #region Either

        public static void RunEitherMonadSample()
        {
            var actionOne = new ActionOne();
            var actionTwo = new ActionTwo();

            actionOne
                .Bind(0)
                .Bind(actionTwo);
        }

        #endregion

        #region Saga

        private static ActivityHost[] s_processes = Array.Empty<ActivityHost>();

        public static void RunSagaEngineSample()
        {
            var routingSlip = new RoutingSlip(new WorkItem[]
            {
                new WorkItem<ReserveCarActivity>(new WorkItemArguments{{"vehicleType", "Compact"}}),
                new WorkItem<ReserveHotelActivity>(new WorkItemArguments{{"roomType", "Suite"}}),
                new WorkItem<ReserveFlightActivity>(new WorkItemArguments{{"destination", "DUS"}})
            });


            // Imagine these being completely separate processes with queues between them.
            s_processes = new ActivityHost[]
            {
                new ActivityHost<ReserveCarActivity>(Send),
                new ActivityHost<ReserveHotelActivity>(Send),
                new ActivityHost<ReserveFlightActivity>(Send)
            };

            // Hand off to the first address.
            Send(routingSlip.ProgressUri, routingSlip);
        }

        private static void Send(Uri? uri, RoutingSlip routingSlip)
        {
            // This is effectively the network dispatch.
            foreach (ActivityHost process in s_processes)
            {
                if (process.AcceptMessage(uri, routingSlip))
                {
                    break;
                }
            }
        }

        #endregion
    }
}
