using Items.StateMachine.V3.States;

namespace Items.StateMachine.V3.Tasks
{
    public interface ITaskBase<in TInput, out TOutput>
        where TInput: StateBase 
        where TOutput: StateBase
    {
        TOutput Do(TInput state);
    }
}