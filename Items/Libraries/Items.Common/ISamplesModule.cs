namespace Items.Common
{
    public interface ISamplesModule
    {
        string ModuleName { get; }


        SampleCollection ProvideSamples();
    }
}
