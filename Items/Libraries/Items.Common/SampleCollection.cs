using System;
using System.Collections.Generic;
using Items.Common.Utils;

namespace Items.Common
{
    public sealed class SampleCollection : Dictionary<string, Action>
    {
        private static readonly PrefixLogger Logger = PrefixLogger.Create(nameof(SampleCollection));


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
                Logger.Message($"Run sample '{sampleId}'.");
                sample();
                Logger.Message($"Sample '{sampleId}' was finished.{Environment.NewLine}");
            }
        }

        public void Run(string sampleId)
        {
            if (TryGetValue(sampleId, out Action sample))
            {
                Logger.Message($"Run sample '{sampleId}'.");
                sample();
                Logger.Message($"Sample '{sampleId}' was finished.{Environment.NewLine}");
            }

            throw new ArgumentException(
                $"Failed to find sample '{sampleId}'.", nameof(sampleId)
            );
        }
    }
}
