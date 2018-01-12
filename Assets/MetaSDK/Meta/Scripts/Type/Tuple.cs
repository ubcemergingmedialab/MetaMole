namespace Meta
{
    public class Tuple <T1, T2, T3>
    {
        public T1 Item1 { get; protected set; }

        public T2 Item2 { get; protected set; }

        public T3 Item3 { get; protected set; }

        public Tuple(T1 item1, T2 item2, T3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }
    }
}