namespace Items.StateMachine.States
{
    public sealed class FinalStatefulTask : IStatefulTask<State>
    {
        public bool IsFinal { get; } = true;


        public FinalStatefulTask()
        {
        }

        #region IStatefullTask<State> Implementation

        IStatefulTask<State> IStatefulTask<State>.DoAction(State state)
        {
            return new FinalStatefulTask();
        }

        #endregion
    }
}
