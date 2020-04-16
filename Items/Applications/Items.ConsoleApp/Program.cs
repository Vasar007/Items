using System;
using Items.Common;
using Items.Common.Logging;
using Items.RollbackEngine;
using Items.StateMachine;

namespace Items.ConsoleApp
{
    internal static class Program
    {
        private static readonly ILogger Logger = LoggerFactory.CreateLoggerFor(typeof(Program));


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
            moduleRunner.RegisterModule(new StateMachineSamples());

            moduleRunner.Run();
        }
    }
}
