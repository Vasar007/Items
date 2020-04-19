namespace Items.StateMachine.V1.States
{
    public sealed class TaskC : IStatefulTask<State>
    {
        public bool IsFinal { get; } = false;


        public TaskC()
        {
        }

        #region IStatefullTask<State> Implementation

        IStatefulTask<State> IStatefulTask<State>.DoAction(State state)
        {
            if (state.B > 10)
            {
                state.B *= 10;
                state.A = 24;
            }

            return new TaskB();
        }

        #endregion
    }
}
