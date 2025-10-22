namespace Macaron.Functional;

public static class NelExtensions
{
    public static Seq<T> ToSeq<T>(this Nel<T> nel) => new Seq<T>.Node(nel.Head, nel.Tail);
}
