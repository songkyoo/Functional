namespace Macaron.Functional;

public static class SeqExtensions
{
    public static void Deconstruct<T>(this Seq<T> seq, out T head, out Seq<T>? tail)
    {
        head = seq.Head;
        tail = seq.Tail;
    }
}
