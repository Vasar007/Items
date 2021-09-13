using System.Collections.Generic;
using Items.Common.Logging;
using Items.StateMachine.V4.Builders;
using Items.StateMachine.V4.Samples;
using Items.StateMachine.V4.Tasks;
using Items.StateMachine.V4.Tasks.Default;
using Items.StateMachine.V4.Tasks.Default.WithRollback;

namespace Items.StateMachine.V4
{
    public static class StateMachineV4Samples
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor(typeof(StateMachineV4Samples));

        public static void RunSimpleStateMachineSample()
        {
            var initialState = new Context(42, 1337);
            var initialAction = new TaskC();

            var transitions = new Dictionary<StateId, IStatefulTask<Context, StateId>>
            {
                { StateId.StateA, new TaskA() },
                { StateId.StateB, new TaskB() },
                { StateId.StateC, new TaskC() },
                { StateId.Final, FinalStatefulTask<Context>.Create(StateId.Final) }
            };

            Logger.Message($"Initial state: {initialState}");
            Logger.Message("Starting performing.");
            Context finalState = StateMachineHelper.Perform(
                initialState, initialAction, transitions
            );
            Logger.Message($"Final state: '{finalState}'.");

            Logger.SkipLine();
            var initialAction2 = new TaskA();

            Logger.Message($"Initial state: {initialState}");
            Logger.Message("Starting performing.");
            Context finalState2 = StateMachineHelper.Perform(
                initialState, initialAction2, transitions
            );
            Logger.Message($"Final state: '{finalState2}'.");
        }

        public static void RunStateMachineUntilFinishEnumeratorSample()
        {
            var initialState = new Context(42, 1337);
            IStatefulTask<Context, StateId> initialAction = new TaskC();

            Logger.Message($"Initial state: {initialState}");
            Logger.Message("Starting performing.");
            Context finalState = initialAction
                .FillWithTransitionsTable(FillExecutor)
                .PerformUntilFinalState(initialState)
                .Execute();

            Logger.Message($"Final state: {finalState}");

            Logger.SkipLine();
            IStatefulTask<Context, StateId> initialAction2 = new TaskA();

            Logger.Message($"Initial state: {initialState}");
            Logger.Message("Starting performing.");
            Context finalState2 = initialAction2
                .FillWithTransitionsTable(FillExecutor)
                .PerformUntilFinalState(initialState)
                .CatchExceptions()
                .Execute();

            Logger.Message($"Final state: {finalState2}");
        }

        public static void RunStateMachineWithRollbackUntilFinishEnumeratorSample()
        {
            var initialState = new Context(42, 1337);
            IStatefulTaskWithRollback<Context, StateId> initialAction = new TaskCWithRollback();

            Logger.Message($"Initial state: {initialState}");
            Logger.Message("Starting performing with rollback.");
            Context finalState = initialAction
                .FillWithTransitionsTable(FillExecutorWithRollback)
                .PerformUntilFinalState(initialState)
                .WithRollbackOnException()
                .Execute();

            Logger.Message($"Final state: {finalState}");

            Logger.SkipLine();
            IStatefulTaskWithRollback<Context, StateId> initialAction2 = new TaskAWithRollback();

            Logger.Message($"Initial state: {initialState}");
            Logger.Message("Starting performing with rollback.");
            Context finalState2 = initialAction2
                .FillWithTransitionsTable(FillExecutorWithRollback)
                .PerformUntilFinalState(initialState)
                .WithRollbackOnException()
                .CatchExceptions()
                .Execute();

            Logger.Message($"Final state: {finalState2}");
        }

        private static IStateMachineBuilderWithoutStateId<Context, StateId, IStatefulTask<Context, StateId>> FillExecutor(
            IStatefulTask<Context, StateId> initialTask)
        {
            return initialTask.AsInitial(StateId.Initial)
                .On(StateId.StateA).GoTo(new TaskA())
                .On(StateId.StateB).GoTo(new TaskB())
                .On(StateId.StateC).GoTo(new TaskC())
                .OnFinalGoToSelfLoop(StateId.Final);
        }

        private static IStateMachineBuilderWithoutStateId<Context, StateId, IStatefulTaskWithRollback<Context, StateId>> FillExecutorWithRollback(
            IStatefulTaskWithRollback<Context, StateId> initialTask)
        {
            return initialTask.AsInitial(StateId.Initial)
                .On(StateId.StateA).GoTo(new TaskAWithRollback())
                .On(StateId.StateB).GoTo(new TaskBWithRollback())
                .On(StateId.StateC).GoTo(new TaskCWithRollback())
                .OnFinalGoToSelfLoop(StateId.Final);
        }
    }
}
