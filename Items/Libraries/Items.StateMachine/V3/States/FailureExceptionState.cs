using System.Runtime.ExceptionServices;

namespace Items.StateMachine.V3.States
{
    public class FailureExceptionState : FailureState
    {
        public ExceptionDispatchInfo? Exception { get; set; }

        public override string ToString()
        {
            return $"[FailureExceptionState] {nameof(Exception)}: {Exception?.SourceException.Message}";
        }
    }
}