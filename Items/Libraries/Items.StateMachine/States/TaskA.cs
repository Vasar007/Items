namespace Items.StateMachine.States
{
    public sealed class TaskA : IStatefulTask<State>
    {
        public bool IsFinal { get; } = false;


        public TaskA()
        {
        }

        #region IStatefullTask<State> Implementation

        IStatefulTask<State> IStatefulTask<State>.DoAction(State state)
        {
            if (state.A > 10)
            {
                state.A *= 10;
                state.B = 42;
            }

            return new TaskC();
        }

        #endregion
    }
}
