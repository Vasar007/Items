using Items.Common;
using Items.Common.Logging;
using Items.StateMachine.V1;

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
                { "StateMachine.SimpleStateMachineV1", StateMachineV1Samples.RunSimpleStateMachineV1Sample },
                { "StateMachine.UntilFinishEnumeratorV1", StateMachineV1Samples.RunStateMachineUntilFinishEnumeratorV1Sample }
            };
        }

        #endregion
    }
}
