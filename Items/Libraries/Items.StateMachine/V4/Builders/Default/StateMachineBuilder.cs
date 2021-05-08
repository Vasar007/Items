using System;
using System.Collections.Generic;
using Acolyte.Assertions;
using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4.Builders.Default
{
    internal sealed class StateMachineBuilder<TContext, TStateId, TStatefulTask> :
        IStateMachineBuilderWithoutStateId<TContext, TStateId, TStatefulTask>,
        IStateMachineBuilderWithStateId<TContext, TStateId, TStatefulTask>
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>
    {
        // Implement two interfaces in one class to reuse single instance during state machine building.

        public TStatefulTask InitialTask { get; }

        private readonly Dictionary<TStateId, TStatefulTask> _transitionsTable;
        public IReadOnlyDictionary<TStateId, TStatefulTask> TransitionsTable => _transitionsTable;

        public TStateId StateId { get; private set; }

        public StateMachineBuilder(
            TStatefulTask initialTask,
            TStateId initialStateId,
            Dictionary<TStateId, TStatefulTask> transitionsTable)
        {
            InitialTask = initialTask.ThrowIfNull(nameof(initialTask));
            StateId = initialStateId;
            _transitionsTable = transitionsTable.ThrowIfNull(nameof(transitionsTable));

            // Add initial stateful task.
            _transitionsTable.TryAdd(StateId, InitialTask);
        }

        public IStateMachineBuilderWithStateId<TContext, TStateId, TStatefulTask> RememberStateId(
            TStateId stateId)
        {
            StateId = stateId;
            return this;
        }

        public IStateMachineBuilderWithoutStateId<TContext, TStateId, TStatefulTask> AddStatefulTask(
            TStatefulTask statefulTask)
        {
            if (statefulTask is null)
            {
                throw new ArgumentException(
                    "Invalid stateful task to add in state machine.", nameof(statefulTask)
                );
            }

            _transitionsTable.Add(StateId, statefulTask);
            return this;
        }
    }

    internal static class StateMachineBuilder<TContext>
    {
        public static StateMachineBuilder<TContext, TStateId, TStatefulTask> CreateNew<TStateId, TStatefulTask>(
            TStatefulTask initialTask,
            TStateId initialStateId)
            where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            return new StateMachineBuilder<TContext, TStateId, TStatefulTask>(
                initialTask: initialTask,
                initialStateId: initialStateId,
                transitionsTable: new Dictionary<TStateId, TStatefulTask>()
            );
        }
    }
}
