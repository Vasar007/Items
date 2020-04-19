using Items.StateMachine.V2.States;

namespace Items.StateMachine.V2.Executors.Fillers
{
    public sealed class DefaultStateMachineFiller<TState, TStateId> :
        IStateMachineFiller<TState, TStateId>
        where TState : class
    {
        public IStateMachineExecutor<TState, TStateId> Executor { get; }

        public TStateId StateIdToFill { get; set; }


        public DefaultStateMachineFiller(
            IStateMachineExecutor<TState, TStateId> executor,
            TStateId stateIdToFill)
        {
            Executor = executor;
            StateIdToFill = stateIdToFill;
        }

        public IStateMachineExecutor<TState, TStateId> FillExecutor(
            IStatefulTask<TState, TStateId> statefulTask)
        {
            Executor[StateIdToFill] = statefulTask;
            return Executor;
        }
    }

    public static class DefaultStateMachineFiller
    {
        public static DefaultStateMachineFiller<TState, TStateId> Create<TState, TStateId>(
            IStateMachineExecutor<TState, TStateId> executor,
            TStateId stateIdToFill)
            where TState : class
        {
            return new DefaultStateMachineFiller<TState, TStateId>(
                executor: executor,
                stateIdToFill: stateIdToFill
            );
        }
    }
}
