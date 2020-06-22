namespace Items.StateMachine.V1.States
{
    public interface IStatefulTask<TState>
        where TState : class  // This constrain required because state can be changed inside DoAction method.
    {
        bool IsFinal { get; }


        IStatefulTask<TState> DoAction(TState state);
    }
}
