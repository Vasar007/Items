using Items.StateMachine.V3.States;

namespace Items.StateMachine.V3.Tasks
{
    public class DeterministicTask : ITaskBase<StateBase, StateBase>
    {
        public StateBase Do(StateBase state)
        {
            if(state is StateA stateA)
                return new StateB() {B = stateA.A + 1};

            return new FailureMessageState() {Message = "Invalid state"};
        }

    }
}