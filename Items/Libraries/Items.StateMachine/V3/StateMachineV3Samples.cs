using System.Collections.Generic;
using Items.Common.Logging;
using Items.StateMachine.V3.Executors;
using Items.StateMachine.V3.States;
using Items.StateMachine.V3.Tasks;

namespace Items.StateMachine.V3
{
    public class StateMachineV3Samples
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor(typeof(StateMachineV3Samples));

        public static void RunSimple()
        {
            StateBase initialState = new StateA() {A = 1};
            IReadOnlyCollection<ITaskBase<StateBase, StateBase>> tasks = new ITaskBase<StateBase, StateBase>[]
            {
                new DeterministicTask(),
                new DryTask(),
                new GenericInputTask()
            };

            Logger.Message($"InitialState: {initialState}");
            var terminationState = SimpleExecutor.Run(initialState, tasks);
            Logger.Message($"FinalState: {terminationState}");
            
            initialState = new StateA() {A = 2};
            
            Logger.Message($"InitialState: {initialState}");
            terminationState = SimpleExecutor.Run(initialState, tasks);
            Logger.Message($"FinalState: {terminationState}");


        }
    }
}