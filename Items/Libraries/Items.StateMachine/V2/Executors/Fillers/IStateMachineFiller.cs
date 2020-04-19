using Items.StateMachine.V2.States;

namespace Items.StateMachine.V2.Executors.Fillers
{
    public interface IStateMachineFiller<TState, TStateId>
        where TState : class
    {
        IStateMachineExecutor<TState, TStateId> Executor { get; }

        TStateId StateIdToFill { get; set; }

        IStateMachineExecutor<TState, TStateId> FillExecutor(
            IStatefulTask<TState, TStateId> statefulTask);
    }
}
