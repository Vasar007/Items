using System;
using System.Runtime.ExceptionServices;
using Items.StateMachine.V3.States;

namespace Items.StateMachine.V3.Tasks
{
    public class DryTask : ITaskBase<StateBase, StateBase>
    {
        public StateBase Do(StateBase state)
        {
            try
            {
                if (state is StateB stateB &&
                    stateB.B % 2 == 0)
                    return new StateA() {A = stateB.B / 2};
                
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                return new FailureExceptionState()
                {
                    Exception = ExceptionDispatchInfo.Capture(e)
                };
            }
        }
    }
}