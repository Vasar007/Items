using Acolyte.Assertions;

namespace Items.Common
{
    internal sealed class DefaultSamplesRunner : ISamplesRunner
    {
        private readonly SampleCollection _samples;


        public DefaultSamplesRunner(ISamplesModule samplesModules)
        {
            samplesModules.ThrowIfNull(nameof(samplesModules));

            _samples = samplesModules.ProvideSamples();
        }

        #region ISamplesRunner Implementation

        public void RunAllSamples()
        {
            _samples.RunAll();
        }

        public void RunSample(string sampleId)
        {
            _samples.Run(sampleId);
        }

        #endregion
    }
}
