using Items.Common;
using Items.Common.Logging;
using Items.StateMachine.V1;
using Items.StateMachine.V2;
using Items.StateMachine.V3;

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
                { "StateMachine.SimpleStateMachineV1", StateMachineV1Samples.RunSimpleStateMachineSample },
                { "StateMachine.UntilFinishEnumeratorV1", StateMachineV1Samples.RunStateMachineUntilFinishEnumeratorSample },
                { "StateMachine.SimpleStateMachineV2", StateMachineV2Samples.RunSimpleStateMachineSample },
                { "StateMachine.UntilFinishEnumeratorV2", StateMachineV2Samples.RunStateMachineUntilFinishEnumeratorSample },
                { "StateMachine.SimpleStateMachineV3", StateMachineV3Samples.RunSimple }
            };
        }

        #endregion
    }
}
