using System;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Items.Common
{
    internal sealed class DefaultModuleRunner : IModuleRunner
    {
        private readonly List<ISamplesModule> _samplesModules;


        public DefaultModuleRunner()
        {
            _samplesModules = new List<ISamplesModule>();
        }

        #region IModuleRunner Implementation

        public void RegisterModule(ISamplesModule samplesModule)
        {
            samplesModule.ThrowIfNull(nameof(samplesModule));

            _samplesModules.Add(samplesModule);
        }

        public void Run()
        {
            Run(InfrastructureFactory.CreateSamplesRunner);
        }

        public void Run(Func<ISamplesModule, ISamplesRunner> runnerFactory)
        {
            runnerFactory.ThrowIfNull(nameof(runnerFactory));

            foreach (ISamplesModule samplesModule in _samplesModules)
            {
                ISamplesRunner samplesRunner = runnerFactory(samplesModule);
                RunInternal(samplesRunner);
            }
        }

        #endregion

        private void RunInternal(ISamplesRunner samplesRunner)
        {
            samplesRunner.ThrowIfNull(nameof(samplesRunner));

            samplesRunner.RunAllSamples();
        }
    }
}
