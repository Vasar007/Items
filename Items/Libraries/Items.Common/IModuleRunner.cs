using System;

namespace Items.Common
{
    public interface IModuleRunner
    {
        void RegisterModule(ISamplesModule samplesModule);

        void Run();

        void Run(Func<ISamplesModule, ISamplesRunner> runnerFactory);
    }
}
