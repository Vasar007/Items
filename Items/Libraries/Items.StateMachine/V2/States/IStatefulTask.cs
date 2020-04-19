namespace Items.StateMachine.V2.States
{
    public interface IStatefulTask<TState, TStateId>
        where TState : class
    {
        bool IsFinal { get; }


        TStateId DoAction(TState state);
    }
}
