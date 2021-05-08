using Acolyte.Assertions;

namespace Items.StateMachine.V4.Tasks.Default
{
    internal sealed class StatefulTaskWrapper<TContext, TStateId> :
        NonFinalStatefulTaskBase<TContext, TStateId>
    {
        private readonly StatefulTaskDoAction<TContext, TStateId> _doAction;

        public StatefulTaskWrapper(
            StatefulTaskDoAction<TContext, TStateId> doAction)
        {
            _doAction = doAction.ThrowIfNull(nameof(doAction));
        }

        #region NonFinalStatefulTaskBase<TContext, TStateId> Overridden Methods

        protected override TStateId DoActionInternal(TContext context)
        {
            return _doAction(context);
        }

        #endregion
    }

    internal static class StatefulTaskWrapper
    {
        public static StatefulTaskWrapper<TContext, TStateId> Create<TContext, TStateId>(
            StatefulTaskDoAction<TContext, TStateId> doAction)
        {
            return new StatefulTaskWrapper<TContext, TStateId>(doAction);
        }
    }
}
