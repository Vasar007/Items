using Items.Common;
using Items.StateMachine.StateMachine;
using Items.StateMachine.States;

namespace Items.StateMachine
{
    public sealed class StateMachineSamples : ISamplesModule
    {
        public string ModuleName { get; } = nameof(StateMachineSamples);


        public StateMachineSamples()
        {
        }

        #region ISamplesModule Implementation

        public SampleCollection ProvideSamples()
        {
            return new SampleCollection
            {
                { "StateMachine.SimpleStateMachine", RunSimpleStateMachineSample }
            };
        }

        #endregion

        public static void RunSimpleStateMachineSample()
        {
            var initialState = new State(42, 1337);
            var initialAction = new ActionA();

            var stateMachine = new StateMachine<State>();

            State finalState = stateMachine.Perform(initialState, initialAction);

            var initialAction2 = new ActionC();

            State finalState2 = stateMachine.Perform(initialState, initialAction2);
        }
    }
}
