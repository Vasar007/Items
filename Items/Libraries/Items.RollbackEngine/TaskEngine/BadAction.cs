using System;

namespace Items.RollbackEngine.TaskEngine
{
    public sealed class BadAction : IStatefulTask<State>
    {
        public BadAction()
        {
        }

        #region IStatefulTask<State> Implementation

        public State DoAction(State state)
        {
            throw new NotImplementedException();
        }

        public State RollbackSafe(State state)
        {
            return state;
        }

        #endregion
    }
}
