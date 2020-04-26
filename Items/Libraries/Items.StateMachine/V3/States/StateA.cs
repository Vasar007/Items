namespace Items.StateMachine.V3.States
{
    public class StateA : StateBase
    {
        public int A { get; set; }

        public override string ToString()
        {
            return $"[StateA] {nameof(A)}: {A}";
        }
    }
}