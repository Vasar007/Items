namespace Items.StateMachine.V1.States
{
    public interface IStatefulTask<TState>
        where TState : class
    {
        bool IsFinal { get; }


        IStatefulTask<TState> DoAction(TState state);
    }
}
