using System;
using Items.Common;
using Items.RollbackEngine.Either;
using Items.RollbackEngine.Saga;
using Items.RollbackEngine.Simple;

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
                { "RollbackEngine.Simple", RunSimpleSample },
                { "RollbackEngine.TaskEngine", RunTaskEngineSample },
                { "RollbackEngine.Either", RunEitherMonadSample },
                { "RollbackEngine.Saga", RunSagaEngineSample }
            };
        }

        #endregion

        #region Simple

        public static void RunSimpleSample()
        {
            var actionOne = new SimpleRollbackActionOne();
            var actionTwo = new SimpleRollbackActionTwo();

            using var scope = new RollbackScope<int>(
                continueRollbackOnFailed: true, rollbackParameter: 42
            );

            scope.Add(actionOne);
            scope.Add(actionTwo);

            scope.TryRollbackSafe();
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

        private static ActivityHost[] _processes = Array.Empty<ActivityHost>();

        public static void RunSagaEngineSample()
        {
            var routingSlip = new RoutingSlip(new WorkItem[]
            {
                new WorkItem<ReserveCarActivity>(new WorkItemArguments{{"vehicleType", "Compact"}}),
                new WorkItem<ReserveHotelActivity>(new WorkItemArguments{{"roomType", "Suite"}}),
                new WorkItem<ReserveFlightActivity>(new WorkItemArguments{{"destination", "DUS"}})
            });


            // Imagine these being completely separate processes with queues between them.
            _processes = new ActivityHost[]
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
            foreach (ActivityHost process in _processes)
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
