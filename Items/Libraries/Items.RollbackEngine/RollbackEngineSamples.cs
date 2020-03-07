using Items.Common;

namespace Items.RollbackEngine
{
    public sealed class RollbackEngineSamples : ISamplesModule
    {
        public RollbackEngineSamples()
        {
        }

        #region ISamplesModule Implementation

        public SampleCollection ProvideSamples()
        {
            return new SampleCollection
            {
                { "RollbackEngine.TaskEngine", RunTaskEngineSample }
            };
        }

        #endregion

        public static void RunTaskEngineSample()
        {
            TaskEngine te = new TaskEngine();
            te.Demo();
        }
    }
}
