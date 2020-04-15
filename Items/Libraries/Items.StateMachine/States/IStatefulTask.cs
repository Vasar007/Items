namespace Items.StateMachine.States
{
    public interface IStatefulTask<TState>
    {
        bool IsFinal { get; }


        IStatefulTask<TState> DoAction(TState state);
    }
}
