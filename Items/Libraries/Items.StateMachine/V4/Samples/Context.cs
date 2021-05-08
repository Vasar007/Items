namespace Items.StateMachine.V4.Samples
{
    public sealed class Context
    {
        public int A { get; set; }

        public int B { get; set; }


        public Context(
            int a,
            int b)
        {
            A = a;
            B = b;
        }

        #region Object Overridden

        public override string ToString()
        {
            return $"[{nameof(Context)}] {nameof(A)}: {A.ToString()}, {nameof(B)}: {B.ToString()}";
        }

        #endregion
    }
}
