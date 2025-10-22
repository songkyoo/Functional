namespace Macaron.Functional;

public static class SeqExtensions
{
    public static Maybe<Nel<T>> ToNel<T>(this Seq<T> seq) => seq switch
    {
        Seq<T>.Node node => Maybe.Just(new Nel<T>(node.Head, node.Tail)),
        Seq<T>.Empty => Maybe.Nothing(),
        _ => throw new InvalidOperationException("Unexpected sequence type."),
    };
}
