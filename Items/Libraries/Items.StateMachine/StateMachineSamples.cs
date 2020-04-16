using Items.Common;
using Items.Common.Logging;
using Items.StateMachine.Common;
using Items.StateMachine.States;

namespace Items.StateMachine
{
    public sealed class StateMachineSamples : ISamplesModule
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor<StateMachineSamples>();

        public string ModuleName { get; } = nameof(StateMachineSamples);


        public StateMachineSamples()
        {
        }

        #region ISamplesModule Implementation

        public SampleCollection ProvideSamples()
        {
            return new SampleCollection
            {
                { "StateMachine.SimpleStateMachine", RunSimpleStateMachineSample },
                { "StateMachine.UntilFinishEnumerator", RunStateMachineUntilFinishEnumeratorSample }
            };
        }

        #endregion

        public static void RunSimpleStateMachineSample()
        {
            var initialState = new State(42, 1337);
            var initialAction = new TaskA();

            State finalState = StateMachineHelper.PerformCasual(initialState, initialAction);

            Logger.SkipLine();
            var initialAction2 = new TaskC();

            State finalState2 = StateMachineHelper.PerformCasual(initialState, initialAction2);
        }

        public static void RunStateMachineUntilFinishEnumeratorSample()
        {
            var initialState = new State(42, 1337);
            var initialAction = new TaskA();

            Logger.Message($"Initial state: {initialState}");
            Logger.Message("Starting performing.");
            State finalState = initialAction.PerformUntilFinalState(initialState).Execute();
            Logger.Message($"Final state: {finalState}");

            Logger.SkipLine();
            var initialAction2 = new TaskC();

            Logger.Message($"Initial state: {initialState}");
            Logger.Message("Starting performing.");
            State finalState2 = initialAction2.PerformUntilFinalState(initialState).Execute();
            Logger.Message($"Final state: {finalState2}");
        }
    }
}
