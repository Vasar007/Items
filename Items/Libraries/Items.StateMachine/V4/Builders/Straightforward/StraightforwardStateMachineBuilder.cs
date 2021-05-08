using System;
using System.Collections.Generic;
using Acolyte.Assertions;
using Items.StateMachine.V4.Tasks.Straightforward;

namespace Items.StateMachine.V4.Builders.Straightforward
{
    internal sealed class StraightforwardStateMachineBuilder<TContext, TStraightforwardStatefulTask> :
        IStraightforwardStateMachineBuilder<TContext, TStraightforwardStatefulTask>
        where TStraightforwardStatefulTask : class, IStraightforwardStatefulTask<TContext>
    {
        // Reuse single instance during straightforward state machine building.

        public TStraightforwardStatefulTask InitialTask { get; }

        private readonly List<TStraightforwardStatefulTask> _transitionsList;
        public IReadOnlyList<TStraightforwardStatefulTask> TransitionsList => _transitionsList;

        public StraightforwardStateMachineBuilder(
            TStraightforwardStatefulTask initialTask,
            List<TStraightforwardStatefulTask> transitionsList)
        {
            InitialTask = initialTask.ThrowIfNull(nameof(initialTask));
            _transitionsList = transitionsList.ThrowIfNull(nameof(transitionsList));

            // Add initial stateful task.
            _transitionsList.Add(InitialTask);
        }

        public IStraightforwardStateMachineBuilder<TContext, TStraightforwardStatefulTask> AddStatefulTask(TStraightforwardStatefulTask statefulTask)
        {
            if (statefulTask is null)
            {
                throw new ArgumentException(
                    "Invalid straightforward stateful task to add in state machine.",
                    nameof(statefulTask)
                );
            }

            _transitionsList.Add(statefulTask);
            return this;
        }
    }

    internal static class StraightforwardStateMachineBuilder<TContext>
    {
        public static StraightforwardStateMachineBuilder<TContext, TStraightforwardStatefulTask> CreateNew<TStraightforwardStatefulTask>(
            TStraightforwardStatefulTask initialTask)
            where TStraightforwardStatefulTask : class, IStraightforwardStatefulTask<TContext>
        {
            return new StraightforwardStateMachineBuilder<TContext, TStraightforwardStatefulTask>(
                initialTask: initialTask,
                transitionsList: new List<TStraightforwardStatefulTask>()
            );
        }
    }
}
