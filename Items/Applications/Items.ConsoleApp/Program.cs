using System;
using Items.Common;
using Items.Common.Utils;
using Items.RollbackEngine;

namespace Items.ConsoleApp
{
    internal sealed class Program
    {
        private static readonly PrefixLogger Logger = PrefixLogger.Create(nameof(Program));


        private static int Main(string[] args)
        {
            try
            {
                Logger.Message("Console application started.");

                RunModules();

                return 0;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, $"Exception occurred in {nameof(Main)} method.");

                return -1;
            }
            finally
            {
                Logger.Message("Console application stopped.");
                Logger.Message("Press any key to close this window...");
                Console.ReadKey();
            }
        }

        private static void RunModules()
        {
            IModuleRunner moduleRunner = InfrastructureFactory.CreateModuleRunner();

            moduleRunner.RegisterModule(new RollbackEngineSamples());

            moduleRunner.Run();
        }
    }
}
