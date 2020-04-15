namespace Items.StateMachine.States
{
    public sealed class ActionA : IStatefulTask<State>
    {
        public bool IsFinal { get; } = false;


        public ActionA()
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

            return new ActionC();
        }

        #endregion
    }
}
