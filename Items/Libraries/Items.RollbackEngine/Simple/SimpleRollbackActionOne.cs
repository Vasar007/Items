using Items.Common.Logging;

namespace Items.RollbackEngine.Simple
{
    internal sealed class SimpleRollbackActionOne : IRollbackAction<int>
    {
        private static readonly ILogger Logger =
         LoggerFactory.CreateLoggerFor<SimpleRollbackActionOne>();


        public SimpleRollbackActionOne()
        {
        }

        #region IRollbackAction<int> Implementation

        public bool TryRollback(int rollbackParameter)
        {
            Logger.Message(
                $"Rollback action two with parameter: '{rollbackParameter.ToString()}'."
            );
            return true;
        }

        #endregion
    }
}
