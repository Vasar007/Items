using System;
using System.Collections.Generic;
using Acolyte.Assertions;
using Items.Common.Logging;

namespace Items.Common
{
    internal sealed class DefaultModuleRunner : IModuleRunner
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor<DefaultModuleRunner>();

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
            Logger.Message($"Sample module '{samplesModule.ModuleName}' was registered to run.");
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
                Logger.Message($"Run sample module '{samplesModule.ModuleName}'.{Environment.NewLine}");
                ISamplesRunner samplesRunner = runnerFactory(samplesModule);
                RunInternal(samplesRunner);
                Logger.Message($"Sample module '{samplesModule.ModuleName}' was finished.{Environment.NewLine}");
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
