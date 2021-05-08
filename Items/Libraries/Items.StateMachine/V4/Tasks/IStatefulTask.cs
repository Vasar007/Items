namespace Items.StateMachine.V4.Tasks
{
    public interface IStatefulTask<TContext, TStateId>
    {
        bool IsFinal { get; }

        TStateId DoAction(TContext context);
    }
}
