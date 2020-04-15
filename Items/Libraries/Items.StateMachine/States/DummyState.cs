namespace Items.StateMachine.States
{
    public sealed class DummyState : IStatefulTask<State>
    {
        public bool IsFinal { get; } = true;


        public DummyState()
        {
        }

        #region IStatefullTask<State> Implementation

        IStatefulTask<State> IStatefulTask<State>.DoAction(State state)
        {
            return new DummyState();
        }

        #endregion
    }
}
