namespace Items.StateMachine.V4.Tasks.Straightforward
{
    public abstract class NonFinalStraightforwardStatefulTaskBase<TContext> :
        IStraightforwardStatefulTask<TContext>
    {
        bool IStatefulTask<TContext, int>.IsFinal { get; } = false;

        protected NonFinalStraightforwardStatefulTaskBase()
        {
        }

        int IStatefulTask<TContext, int>.DoAction(TContext context)
        {
            DoActionInternal(context);
            // Logic to determine the next state is up to state machine enumerator.
            return default;
        }

        protected abstract void DoActionInternal(TContext context);
    }
}
