using System;
using Acolyte.Assertions;
using Items.StateMachine.V4.Builders;
using Items.StateMachine.V4.Builders.Default;
using Items.StateMachine.V4.Builders.Straightforward;
using Items.StateMachine.V4.Executors;
using Items.StateMachine.V4.Executors.Safe;
using Items.StateMachine.V4.Executors.Straightforward;
using Items.StateMachine.V4.Executors.UntilFinalState;
using Items.StateMachine.V4.Executors.WithRollback;
using Items.StateMachine.V4.Factories;
using Items.StateMachine.V4.Tasks;
using Items.StateMachine.V4.Tasks.Default;
using Items.StateMachine.V4.Tasks.Default.WithRollback;
using Items.StateMachine.V4.Tasks.Straightforward;
using Items.StateMachine.V4.Tasks.Straightforward.WithRollback;

namespace Items.StateMachine.V4
{
    public delegate TStateId CustomStateMachineAction<TContext, TStateId, TStatefulTask>(TStatefulTask statefulTask, TContext context)
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>;

    public delegate void CustomStraightforwardStateMachineAction<TContext, TStraightforwardStatefulTask>(TStraightforwardStatefulTask statefulTask, TContext context)
            where TStraightforwardStatefulTask : class, IStraightforwardStatefulTask<TContext>;

    public delegate IStateMachineBuilderWithoutStateId<TContext, TStateId, TStatefulTask>
        FillWithTransitionsTableAction<TContext, TStateId, TStatefulTask>(TStatefulTask initialTask)
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>;

    public delegate TStateId StatefulTaskDoAction<TContext, TStateId>(TContext context);

    public delegate void StraightforwardStatefulTaskDoAction<TContext>(TContext context);

    public delegate bool StatefulTaskRollbackAction<TContext>(TContext context);

    public static class StateMachineExtensions
    {
        public static IStateMachineBuilderWithoutStateId<TContext, TStateId, TStatefulTask> FillWithTransitionsTable<TContext, TStateId, TStatefulTask>(
            this TStatefulTask initialTask,
            IStateMachineFactory<TContext, TStateId, TStatefulTask> factory)
            where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            return factory.FillWithTransitionsTable(initialTask);
        }

        public static IStateMachineBuilderWithoutStateId<TContext, TStateId, TStatefulTask> FillWithTransitionsTable<TContext, TStateId, TStatefulTask>(
            this TStatefulTask initialTask,
            FillWithTransitionsTableAction<TContext, TStateId, TStatefulTask> factoryAction)
            where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            return factoryAction(initialTask);
        }

        // To simplify call (no need in generic arguments).
        public static IStateMachineBuilderWithoutStateId<TContext, TStateId, IStatefulTask<TContext, TStateId>> AsInitial<TContext, TStateId>(
            this IStatefulTask<TContext, TStateId> initialTask,
            TStateId initialStateId)
        {
            return initialTask.AsInitial<TContext, TStateId, IStatefulTask<TContext, TStateId>>(initialStateId);
        }

        public static IStateMachineBuilderWithoutStateId<TContext, TStateId, IStatefulTask<TContext, TStateId>> AsInitial<TContext, TStateId>(
            this StatefulTaskDoAction<TContext, TStateId> doAction,
            TStateId initialStateId)
        {
            return StatefulTaskWrapper.Create(doAction).AsInitial<TContext, TStateId, IStatefulTask<TContext, TStateId>>(initialStateId);
        }

        // To simplify call (no need in generic arguments).
        public static IStateMachineBuilderWithoutStateId<TContext, TStateId, IStatefulTaskWithRollback<TContext, TStateId>> AsInitial<TContext, TStateId>(
            this IStatefulTaskWithRollback<TContext, TStateId> initialTask,
            TStateId initialStateId)
        {
            return initialTask.AsInitial<TContext, TStateId, IStatefulTaskWithRollback<TContext, TStateId>>(initialStateId);
        }

        public static IStateMachineBuilderWithoutStateId<TContext, TStateId, IStatefulTaskWithRollback<TContext, TStateId>> AsInitial<TContext, TStateId>(
            this StatefulTaskDoAction<TContext, TStateId> doAction,
            StatefulTaskRollbackAction<TContext>? rollbackAction,
            TStateId initialStateId)
        {
            return StatefulTaskWithRollbackWrapper.Create(doAction, rollbackAction)
                .AsInitial<TContext, TStateId, IStatefulTaskWithRollback<TContext, TStateId>>(initialStateId);
        }

        // To simplify call (no need in generic arguments).
        public static IStraightforwardStateMachineBuilder<TContext, IStraightforwardStatefulTask<TContext>> AsInitial<TContext>(
            this IStraightforwardStatefulTask<TContext> initialTask)
        {
            return initialTask.AsInitial<TContext, IStraightforwardStatefulTask<TContext>>();
        }

        public static IStraightforwardStateMachineBuilder<TContext, IStraightforwardStatefulTask<TContext>> AsInitial<TContext>(
            this StraightforwardStatefulTaskDoAction<TContext> doAction)
        {
            return StraightforwardStatefulTaskWrapper.Create(doAction).AsInitial<TContext, IStraightforwardStatefulTask<TContext>>();
        }

        // To simplify call (no need in generic arguments).
        public static IStraightforwardStateMachineBuilder<TContext, IStraightforwardStatefulTaskWithRollback<TContext>> AsInitial<TContext>(
            this IStraightforwardStatefulTaskWithRollback<TContext> initialTask)
        {
            return initialTask.AsInitial<TContext, IStraightforwardStatefulTaskWithRollback<TContext>>();
        }

        public static IStraightforwardStateMachineBuilder<TContext, IStraightforwardStatefulTaskWithRollback<TContext>> AsInitial<TContext>(
            this StraightforwardStatefulTaskDoAction<TContext> doAction,
            StatefulTaskRollbackAction<TContext>? rollbackAction)
        {
            return StraightforwardStatefulTaskWithRollbackWrapper.Create(doAction, rollbackAction)
                .AsInitial<TContext, IStraightforwardStatefulTaskWithRollback<TContext>>();
        }

        private static IStateMachineBuilderWithoutStateId<TContext, TStateId, TStatefulTask> AsInitial<TContext, TStateId, TStatefulTask>(
            this TStatefulTask initialTask, TStateId initialStateId)
            where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            return StateMachineBuilder<TContext>.CreateNew(initialTask, initialStateId);
        }

        private static IStraightforwardStateMachineBuilder<TContext, TStraightforwardStatefulTask> AsInitial<TContext, TStraightforwardStatefulTask>(
            this TStraightforwardStatefulTask initialTask)
            where TStraightforwardStatefulTask : class, IStraightforwardStatefulTask<TContext>
        {
            return StraightforwardStateMachineBuilder<TContext>.CreateNew(initialTask);
        }

        public static IStateMachineBuilderWithStateId<TContext, TStateId, TStatefulTask> On<TContext, TStateId, TStatefulTask>(
            this IStateMachineBuilderWithoutStateId<TContext, TStateId, TStatefulTask> builder,
            TStateId stateId)
            where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            return builder.RememberStateId(stateId);
        }

        public static IStateMachineBuilderWithoutStateId<TContext, TStateId, TStatefulTask> GoTo<TContext, TStateId, TStatefulTask>(
            this IStateMachineBuilderWithStateId<TContext, TStateId, TStatefulTask> builder,
            TStatefulTask statefulTask)
            where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            return builder.AddStatefulTask(statefulTask);
        }

        public static IStateMachineBuilderWithoutStateId<TContext, TStateId, IStatefulTask<TContext, TStateId>> GoTo<TContext, TStateId>(
            this IStateMachineBuilderWithStateId<TContext, TStateId, IStatefulTask<TContext, TStateId>> builder,
            StatefulTaskDoAction<TContext, TStateId> doAction)
        {
            return builder.AddStatefulTask(StatefulTaskWrapper.Create(doAction));
        }

        public static IStateMachineBuilderWithoutStateId<TContext, TStateId, IStatefulTaskWithRollback<TContext, TStateId>> GoTo<TContext, TStateId>(
            this IStateMachineBuilderWithStateId<TContext, TStateId, IStatefulTaskWithRollback<TContext, TStateId>> builder,
            StatefulTaskDoAction<TContext, TStateId> doAction,
            StatefulTaskRollbackAction<TContext>? rollbackAction)
        {
            var statefulTask = StatefulTaskWithRollbackWrapper.Create(doAction, rollbackAction);
            return builder.AddStatefulTask(statefulTask);
        }

        public static IStraightforwardStateMachineBuilder<TContext, TStraightforwardStatefulTask> ThenGoTo<TContext, TStraightforwardStatefulTask>(
            this IStraightforwardStateMachineBuilder<TContext, TStraightforwardStatefulTask> builder,
            TStraightforwardStatefulTask statefulTask)
            where TStraightforwardStatefulTask : class, IStraightforwardStatefulTask<TContext>
        {
            return builder.AddStatefulTask(statefulTask);
        }

        public static IStraightforwardStateMachineBuilder<TContext, IStraightforwardStatefulTask<TContext>> ThenGoTo<TContext>(
            this IStraightforwardStateMachineBuilder<TContext, IStraightforwardStatefulTask<TContext>> builder,
            StraightforwardStatefulTaskDoAction<TContext> doAction)
        {
            return builder.AddStatefulTask(StraightforwardStatefulTaskWrapper.Create(doAction));
        }

        public static IStraightforwardStateMachineBuilder<TContext, IStraightforwardStatefulTaskWithRollback<TContext>> ThenGoTo<TContext>(
            this IStraightforwardStateMachineBuilder<TContext, IStraightforwardStatefulTaskWithRollback<TContext>> builder,
            StraightforwardStatefulTaskDoAction<TContext> doAction,
            StatefulTaskRollbackAction<TContext>? rollbackAction)
        {
            var statefulTask = StraightforwardStatefulTaskWithRollbackWrapper.Create(doAction, rollbackAction);
            return builder.AddStatefulTask(statefulTask);
        }

        public static IStateMachineBuilderWithoutStateId<TContext, TStateId, IStatefulTask<TContext, TStateId>> OnFinalGoToSelfLoop<TContext, TStateId>(
            this IStateMachineBuilderWithoutStateId<TContext, TStateId, IStatefulTask<TContext, TStateId>> builder,
            TStateId finalStateId)
        {
            return builder.On(finalStateId).GoTo(FinalStatefulTask<TContext>.Create(finalStateId));
        }

        public static IStateMachineBuilderWithoutStateId<TContext, TStateId, IStatefulTaskWithRollback<TContext, TStateId>> OnFinalGoToSelfLoop<TContext, TStateId>(
            this IStateMachineBuilderWithoutStateId<TContext, TStateId, IStatefulTaskWithRollback<TContext, TStateId>> builder,
            TStateId finalStateId)
        {
            return builder.On(finalStateId).GoTo(FinalStatefulTaskWithRollback<TContext>.Create(finalStateId));
        }

        public static IStraightforwardStateMachineBuilder<TContext, IStraightforwardStatefulTask<TContext>> ThenGoToFinalSelfLoop<TContext>(
            this IStraightforwardStateMachineBuilder<TContext, IStraightforwardStatefulTask<TContext>> builder)
        {
            return builder.ThenGoTo(FinalStraightforwardStatefulTask<TContext>.Create());
        }

        public static IStraightforwardStateMachineBuilder<TContext, IStraightforwardStatefulTaskWithRollback<TContext>> ThenGoToFinalSelfLoop<TContext>(
            this IStraightforwardStateMachineBuilder<TContext, IStraightforwardStatefulTaskWithRollback<TContext>> builder)
        {
            return builder.ThenGoTo(FinalStraightforwardStatefulTaskWithRollback<TContext>.Create());
        }

        public static IStateMachineProvider<TContext, TStateId, TStatefulTask> PerformUntilFinalState<TContext, TStateId, TStatefulTask>(
            this IStateMachineBuilderWithoutStateId<TContext, TStateId, TStatefulTask> builder,
            TContext context,
            CustomStateMachineAction<TContext, TStateId, TStatefulTask>? customAction)
            where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            return StateMachineUntilFinalStateProvider.Create(
                context, builder.InitialTask, builder.TransitionsTable, customAction
            );
        }

        public static IStateMachineProvider<TContext, TStateId, TStatefulTask> PerformUntilFinalState<TContext, TStateId, TStatefulTask>(
            this IStateMachineBuilderWithoutStateId<TContext, TStateId, TStatefulTask> builder,
            TContext context)
            where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            return builder.PerformUntilFinalState(context, customAction: null);
        }

        public static IStateMachineProvider<TContext, int, TStraightforwardStatefulTask> PerformUntilFinalState<TContext, TStraightforwardStatefulTask>(
            this IStraightforwardStateMachineBuilder<TContext, TStraightforwardStatefulTask> builder,
            TContext context,
            CustomStraightforwardStateMachineAction<TContext, TStraightforwardStatefulTask>? customAction)
            where TStraightforwardStatefulTask : class, IStraightforwardStatefulTask<TContext>
        {
            return StraightforwardStateMachineProvider.Create(
                context, builder.InitialTask, builder.TransitionsList, customAction
            );
        }

        public static IStateMachineProvider<TContext, int, TStraightforwardStatefulTask> PerformUntilFinalState<TContext, TStraightforwardStatefulTask>(
            this IStraightforwardStateMachineBuilder<TContext, TStraightforwardStatefulTask> builder,
            TContext context)
            where TStraightforwardStatefulTask : class, IStraightforwardStatefulTask<TContext>
        {
            return builder.PerformUntilFinalState(context, customAction: null);
        }

        public static IStateMachineProvider<TContext, TStateId, TStatefulTask> CatchExceptions<TContext, TStateId, TStatefulTask>(
            this IStateMachineProvider<TContext, TStateId, TStatefulTask> provider,
            bool continueExecutionOnFailed,
            Action<Exception>? handler)
            where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            return new StateMachineSafeProvider<TContext, TStateId, TStatefulTask>(
                provider, continueExecutionOnFailed, handler
            );
        }

        public static IStateMachineProvider<TContext, TStateId, TStatefulTask> CatchExceptions<TContext, TStateId, TStatefulTask>(
            this IStateMachineProvider<TContext, TStateId, TStatefulTask> provider,
            bool continueExecutionOnFailed)
            where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            return provider.CatchExceptions(continueExecutionOnFailed, handler: null);
        }

        public static IStateMachineProvider<TContext, TStateId, TStatefulTask> CatchExceptions<TContext, TStateId, TStatefulTask>(
          this IStateMachineProvider<TContext, TStateId, TStatefulTask> provider,
          Action<Exception>? handler)
          where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            return provider.CatchExceptions(continueExecutionOnFailed: false, handler);
        }

        public static IStateMachineProvider<TContext, TStateId, TStatefulTask> CatchExceptions<TContext, TStateId, TStatefulTask>(
            this IStateMachineProvider<TContext, TStateId, TStatefulTask> provider)
            where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            return provider.CatchExceptions(continueExecutionOnFailed: false, handler: null);
        }

        public static IStateMachineProvider<TContext, TStateId, IStatefulTaskWithRollback<TContext, TStateId>> WithRollbackOnException<TContext, TStateId, TStatefulTaskWithRollback>(
            this IStateMachineProvider<TContext, TStateId, TStatefulTaskWithRollback> provider,
            bool continueRollbackOnFailed)
            where TStatefulTaskWithRollback : class, IStatefulTaskWithRollback<TContext, TStateId>
        {
            return new StateMachineWithRollbackProvider<TContext, TStateId, TStatefulTaskWithRollback>(
                provider, continueRollbackOnFailed
            );
        }

        public static IStateMachineProvider<TContext, TStateId, IStatefulTaskWithRollback<TContext, TStateId>> WithRollbackOnException<TContext, TStateId, TStatefulTaskWithRollback>(
        this IStateMachineProvider<TContext, TStateId, TStatefulTaskWithRollback> provider)
        where TStatefulTaskWithRollback : class, IStatefulTaskWithRollback<TContext, TStateId>
        {
            return provider.WithRollbackOnException(continueRollbackOnFailed: true);
        }

        public static TContext Execute<TContext, TStateId, TStatefulTask>(
            this IStateMachineProvider<TContext, TStateId, TStatefulTask> provider)
            where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            provider.ThrowIfNull(nameof(provider));

            using var enumerator = provider.GetStateMachineEnumerator();
            while (enumerator.MoveNext())
            {
                // All actions perform in MoveNext.
            }

            return enumerator.Context;
        }
    }
}
