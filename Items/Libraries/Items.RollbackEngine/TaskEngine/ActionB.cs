namespace Items.RollbackEngine.TaskEngine
{
    public sealed class ActionB : IStatefulTask<State>
    {
        public ActionB()
        {
        }

        #region IStatefulTask<State> Imlementation

        public State DoAction(State state)
        {
            return new State(state.B, state.A);
        }

        public State RollbackSafe(State state)
        {
            return new State(state.B, state.A);
        }

        #endregion
    }
}
