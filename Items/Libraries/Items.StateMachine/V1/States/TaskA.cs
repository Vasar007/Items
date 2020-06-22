using System;

namespace Items.StateMachine.V1.States
{
    public sealed class TaskA : NonFinalStatefulTaskBase<State>
    {
        public TaskA()
        {
        }

        #region NonFinalStatefulTaskBase<State> Overridden Methods

        protected override IStatefulTask<State> DoActionInternal(State state)
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
