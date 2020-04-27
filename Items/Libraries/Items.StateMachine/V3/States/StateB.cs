namespace Items.StateMachine.V3.States
{
    public class StateB : StateBase
    {
        public int B { get; set; }

        public override string ToString()
        {
            return $"[StateB] {nameof(B)}: {B}";
        }
    }
}