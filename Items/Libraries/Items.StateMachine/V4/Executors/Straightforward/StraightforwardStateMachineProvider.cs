using System.Collections.Generic;
using Acolyte.Assertions;
using Items.StateMachine.V4.Tasks.Straightforward;

namespace Items.StateMachine.V4.Executors.Straightforward
{
    internal sealed class StraightforwardStateMachineProvider<TContext, TStraightforwardStatefulTask> :
        StateMachineBaseProvider<TContext, int, TStraightforwardStatefulTask>
        where TStraightforwardStatefulTask : class, IStraightforwardStatefulTask<TContext>
    {
        private readonly TContext _context;
        private readonly TStraightforwardStatefulTask _initialTask;
        private readonly IReadOnlyList<TStraightforwardStatefulTask> _transitionsList;
        private readonly CustomStateMachineAction<int> _customAction;

        public StraightforwardStateMachineProvider(
            TContext context,
            TStraightforwardStatefulTask initialTask,
            IReadOnlyList<TStraightforwardStatefulTask> transitionsList,
            CustomStateMachineAction<int>? customAction)
        {
            _context = context;
            _initialTask = initialTask.ThrowIfNull(nameof(initialTask));
            _transitionsList = transitionsList.ThrowIfNull(nameof(transitionsList));
            _customAction = customAction ?? (doAction => doAction());
        }

        #region IStateMachineProvider<TContext, TStateId, TStatefulTask> Implementation

        public override IStateMachineEnumerator<TContext, int, TStraightforwardStatefulTask> GetStateMachineEnumerator()
        {
            return new StraightforwardStateMachineEnumerator<TContext, TStraightforwardStatefulTask>(
                context: _context,
                initialTask: _initialTask,
                transitionsList: _transitionsList,
                customAction: _customAction
            );
        }

        #endregion
    }

    internal static class StraightforwardStateMachineProvider
    {
        public static StraightforwardStateMachineProvider<TContext, TStraightforwardStatefulTask> Create<TContext, TStraightforwardStatefulTask>(
            TContext context,
            TStraightforwardStatefulTask initialTask,
            IReadOnlyList<TStraightforwardStatefulTask> transitionsList,
            CustomStateMachineAction<int>? customAction)
            where TStraightforwardStatefulTask : class, IStraightforwardStatefulTask<TContext>
        {
            return new StraightforwardStateMachineProvider<TContext, TStraightforwardStatefulTask>(
                context: context,
                initialTask: initialTask,
                transitionsList: transitionsList,
                customAction: customAction
            );
        }
    }
}
