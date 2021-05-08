namespace Items.StateMachine.V4.Tasks.Straightforward
{
    public sealed class FinalStraightforwardStatefulTask<TContext> :
        IStraightforwardStatefulTask<TContext>
    {
        bool IStatefulTask<TContext, int>.IsFinal { get; } = true;

        public FinalStraightforwardStatefulTask()
        {
        }

        public static FinalStraightforwardStatefulTask<TContext> Create()
        {
            return new FinalStraightforwardStatefulTask<TContext>();
        }

        int IStatefulTask<TContext, int>.DoAction(TContext context)
        {
            // Logic to determine the next state is up to state machine enumerator.
            // Enumerator should check "IsFinal" flag and break enumeration of state machine.
            return default;
        }
    }
}
