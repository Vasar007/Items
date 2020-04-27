using Items.StateMachine.V3.States;

namespace Items.StateMachine.V3.Tasks
{
    public class GenericInputTask : ITaskBase<StateBase, StateBase>
    {
        public StateBase Do(StateBase state)
        {
            switch (state)
            {

                case StateA stateA:
                    return new StateB() {B = 42};
                case StateB stateB:
                    return new StateA() { A = stateB.B + 1 };
                
                case FailureExceptionState failureState:
                    return failureState;
                
                default:
                    return new FailureMessageState() {Message = "Invelid state"};
            }
        }
    }
}