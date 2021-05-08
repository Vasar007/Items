using System.Collections;
using System.Collections.Generic;
using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4.Executors
{
    internal abstract class StateMachineBaseProvider<TContext, TStateId, TStatefulTask> :
        IStateMachineProvider<TContext, TStateId, TStatefulTask>
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>
    {
        protected StateMachineBaseProvider()
        {
        }

        #region IStateMachineProvider<TContext, TStateId> Implementation

        public abstract IStateMachineEnumerator<TContext, TStateId, TStatefulTask> GetStateMachineEnumerator();

        #endregion

        #region IEnumerable<TStatefulTask> Implementation

        IEnumerator<TStatefulTask> IEnumerable<TStatefulTask>.GetEnumerator()
        {
            return GetStateMachineEnumerator();
        }

        #endregion

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetStateMachineEnumerator();
        }

        #endregion
    }
}
