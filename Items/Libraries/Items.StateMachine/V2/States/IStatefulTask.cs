namespace Items.StateMachine.V2.States
{
    public interface IStatefulTask<TState, TStateId>
        where TState : class  // This constrain required because state can be changed inside DoAction method.
    {
        bool IsFinal { get; }


        TStateId DoAction(TState state);
    }
}
