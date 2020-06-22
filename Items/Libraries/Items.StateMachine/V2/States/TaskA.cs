using System;

namespace Items.StateMachine.V2.States
{
    public sealed class TaskA : IStatefulTask<State, StateId>
    {
        public bool IsFinal { get; } = false;


        public TaskA()
        {
        }

        #region IStatefullTask<State, StateId> Implementation

        StateId IStatefulTask<State, StateId>.DoAction(State state)
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
