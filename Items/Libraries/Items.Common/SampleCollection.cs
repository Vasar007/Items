using System;
using System.Collections.Generic;

namespace Items.Common
{
    public sealed class SampleCollection : Dictionary<string, Action>
    {
        public SampleCollection()
        {
        }

        public SampleCollection(int capacity)
            : base(capacity)
        {
        }

        public void RunAll()
        {
            foreach ((string sampleId, Action action) in this)
            {
                action();
            }
        }

        public void Run(string sampleId)
        {
            if (TryGetValue(sampleId, out Action sample))
            {
                sample();
            }

            throw new ArgumentException(
                $"Failed to find sample '{sampleId}'.", nameof(sampleId)
            );
        }
    }
}
