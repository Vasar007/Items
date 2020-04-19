using System;
using System.Collections.Generic;
using Items.Common.Logging;

namespace Items.Common
{
    public sealed class SampleCollection : Dictionary<string, Action>
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor<SampleCollection>();


        public SampleCollection()
        {
        }

        public SampleCollection(int capacity)
            : base(capacity)
        {
        }

        public void RunAll()
        {
            foreach ((string sampleId, Action sample) in this)
            {
                RunSafe(sampleId, sample);
            }
        }

        public void Run(string sampleId)
        {
            if (TryGetValue(sampleId, out Action sample))
            {
                RunSafe(sampleId, sample);
            }

            throw new ArgumentException(
                $"Failed to find sample '{sampleId}'.", nameof(sampleId)
            );
        }

        private void RunSafe(string sampleId, Action sample)
        {
            try
            {
                Logger.Message($"Run sample '{sampleId}'.");
                sample();
                Logger.Message($"Sample '{sampleId}' was finished successfully.{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, $"Failed to run sample '{sampleId}'. Skipping it.");
                Logger.Message($"Sample '{sampleId}' was finished with failures.{Environment.NewLine}");
            }
        }
    }
}
