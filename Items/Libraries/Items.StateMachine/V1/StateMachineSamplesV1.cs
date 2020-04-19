using Items.Common.Logging;
using Items.StateMachine.V1.Common;
using Items.StateMachine.V1.States;

namespace Items.StateMachine.V1
{
    public static class StateMachineV1Samples
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor(typeof(StateMachineV1Samples));

        public static void RunSimpleStateMachineV1Sample()
        {
            var initialState = new State(42, 1337);
            var initialAction = new TaskC();

            State finalState = StateMachineHelper.PerformStraightforward(initialState, initialAction);

            Logger.SkipLine();
            var initialAction2 = new TaskA();

            State finalState2 = StateMachineHelper.PerformStraightforward(initialState, initialAction2);
        }

        public static void RunStateMachineUntilFinishEnumeratorV1Sample()
        {
            var initialState = new State(42, 1337);
            var initialAction = new TaskC();

            Logger.Message($"Initial state: {initialState}");
            Logger.Message("Starting performing.");
            State finalState = initialAction
                .PerformUntilFinalState(initialState)
                .Execute();

            Logger.Message($"Final state: {finalState}");

            Logger.SkipLine();
            var initialAction2 = new TaskA();

            Logger.Message($"Initial state: {initialState}");
            Logger.Message("Starting performing.");
            State finalState2 = initialAction2
                .PerformUntilFinalState(initialState)
                .CatchExceptions()
                .Execute();

            Logger.Message($"Final state: {finalState2}");
        }
    }
}
