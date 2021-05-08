using Items.Common.Logging;

namespace Items.RollbackEngine.Simple
{
    internal sealed class SimpleRollbackActionTwo : IRollbackAction<int>
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor<SimpleRollbackActionTwo>();


        public SimpleRollbackActionTwo()
        {
        }

        #region IRollbackAction<int> Implementation

        public bool TryRollbackSafe(int rollbackParameter)
        {
            Logger.Message(
                $"Rollback action two with parameter: '{rollbackParameter.ToString()}'."
            );
            return true;
        }

        #endregion
    }
}
