using System;

namespace Items.StateMachine.V2.States
{
    public sealed class TaskA : NonFinalStatefulTaskBase<State, StateId>
    {
        public TaskA()
        {
        }

        #region NonFinalStatefulTaskBase<State, StateId> Overridden Methods

        protected override StateId DoActionInternal(State state)
        {
            if (state.A > 10)
            {
                state.A *= 10;
                state.B = 42;
            }

            throw new Exception("Something goes wrong.");
        }

        #endregion
    }
}
