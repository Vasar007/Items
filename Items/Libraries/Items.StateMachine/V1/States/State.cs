namespace Items.StateMachine.V1.States
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
            return $"[{nameof(State)}] {nameof(A)}: {A.ToString()}, {nameof(B)}: {B.ToString()}";
        }

        #endregion
    }
}
