using System;
using Items.Common;
using Items.RollbackEngine;

namespace Items.ConsoleApp
{
    internal sealed class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                Console.WriteLine("Console application started.");

                RunModules();

                return 0;
            }
            catch (Exception ex)
            {
                string exceptionMessage = $"Exception occurred in {nameof(Main)} method. " +
                                          $"{Environment.NewLine}{ex.ToString()}";
                Console.WriteLine(exceptionMessage);

                return -1;
            }
            finally
            {
                Console.WriteLine("Console application stopped.");
                Console.WriteLine("Press any key to close this window...");
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
