namespace Items.StateMachine.States
{
    public sealed class State
    {
        public int A { get; set; }

        public int B { get; set; }


        public State(
            int a,
            int b)
        {
            A = a;
            B = b;
        }

        #region Object Overridden

        public override string ToString()
        {
            return $"[{nameof(State)}] {nameof(A)}: {A}, {nameof(B)}: {B}";
        }

        #endregion
    }
}
