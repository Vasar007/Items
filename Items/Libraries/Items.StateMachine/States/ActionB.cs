namespace Items.StateMachine.States
{
    public sealed class ActionB : IStatefulTask<State>
    {
        public bool IsFinal { get; } = true;


        public ActionB()
        {
        }

        #region IStatefulTask<State> Imlementation

        IStatefulTask<State> IStatefulTask<State>.DoAction(State state)
        {
            (state.A, state.B) = (state.B, state.A);

            return new DummyState();
        }

        #endregion
    }
}
