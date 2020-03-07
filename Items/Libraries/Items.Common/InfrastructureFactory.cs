using Acolyte.Assertions;

namespace Items.Common
{
    public static class InfrastructureFactory
    {
        public static ISamplesRunner CreateSamplesRunner(ISamplesModule samplesModule)
        {
            samplesModule.ThrowIfNull(nameof(samplesModule));

            return new DefaultSamplesRunner(samplesModule);
        }

        public static IModuleRunner CreateModuleRunner()
        {
            return new DefaultModuleRunner();
        }
    }
}
