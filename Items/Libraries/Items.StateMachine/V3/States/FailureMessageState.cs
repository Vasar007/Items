namespace Items.StateMachine.V3.States
{
    public class FailureMessageState : FailureState
    {
        public string? Message { get; set; }

        public override string ToString()
        {
            return $"[FailureMessageState] {nameof(Message)}: {Message}";
        }
    }
}