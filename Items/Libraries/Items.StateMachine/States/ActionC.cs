namespace Items.StateMachine.States
{
    public sealed class ActionC : IStatefulTask<State>
    {
        public bool IsFinal { get; } = false;


        public ActionC()
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

            return new ActionB();
        }

        #endregion
    }
}
