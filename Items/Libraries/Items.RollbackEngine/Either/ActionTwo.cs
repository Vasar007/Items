﻿using System;
using Items.Common.Logging;

namespace Items.RollbackEngine.Either
{
    internal sealed class ActionTwo : IRollbackAction<int, int>
    {
        private static readonly ILogger Logger = LoggerFactory.CreateLoggerFor<ActionTwo>();


        public ActionTwo()
        {
        }

        #region IRollbackAction<int, int> Implementation

        public int Execute(int parameter)
        {
            if (parameter != 1)
            {
                throw new ArgumentException($"Expected 1 argument, got {parameter.ToString()}.", nameof(parameter));
            }

            Logger.Message("Action two done.");
            return 2;
        }

        public bool TryRollbackSafe()
        {
            Logger.Message("Rollback action two.");
            return true;
        }

        #endregion
    }
}
