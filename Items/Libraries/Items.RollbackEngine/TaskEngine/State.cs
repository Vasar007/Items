namespace Items.RollbackEngine.TaskEngine
{
    public sealed class State
    {
        public int A { get; }

        public int B { get; }


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
