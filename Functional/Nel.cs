namespace Macaron.Functional;

public static class Nel
{
    public static Nel<T> Of<T>(T value) => new(value);

    public static Nel<T> Of<T>(T value1, T value2) => new(value1, Seq.Of(value2));

    public static Nel<T> Of<T>(T value1, T value2, T value3) => new(value1, Seq.Of(value2, value3));

    public static Nel<T> Of<T>(T value1, T value2, T value3, T value4) => new(value1, Seq.Of(value2, value3, value4));

    public static Nel<T> Of<T>(T value1, T value2, T value3, T value4, T value5) => new(value1, Seq.Of(value2, value3, value4, value5));

    public static Nel<T> Of<T>(T value1, T value2, T value3, T value4, T value5, T value6) => new(value1, Seq.Of(value2, value3, value4, value5, value6));

    public static Nel<T> Of<T>(T value1, T value2, T value3, T value4, T value5, T value6, T value7) => new(value1, Seq.Of(value2, value3, value4, value5, value6, value7));

    public static Nel<T> Of<T>(T value1, T value2, T value3, T value4, T value5, T value6, T value7, T value8) => new(value1, Seq.Of(value2, value3, value4, value5, value6, value7, value8));

    public static Nel<T> Of<T>(T value, params T[] values)
    {
        return values == null ? throw new ArgumentNullException(nameof(values)) : new Nel<T>(value, Seq.Of(values));
    }

    public static Nel<T> Cons<T>(T head, Seq<T> tail) => new(head, tail);

    public static Nel<T> Cons<T>(T head1, T head2, Seq<T> tail) => new(head1, Seq.Cons(head2, tail));

    public static Nel<T> Cons<T>(T head1, T head2, T head3, Seq<T> tail) => new(head1, Seq.Cons(head2, head3, tail));

    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, Seq<T> tail) => new(head1, Seq.Cons(head2, head3, head4, tail));

    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, Seq<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, tail));

    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, Seq<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, head6, tail));

    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, T head7, Seq<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, head6, head7, tail));

    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, T head7, T head8, Seq<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, head6, head7, head8, tail));

    public static Nel<T> Cons<T>(T head, Nel<T> tail) => new(head, tail);

    public static Nel<T> Cons<T>(T head1, T head2, Nel<T> tail) => new(head1, Seq.Cons(head2, tail.ToSeq()));

    public static Nel<T> Cons<T>(T head1, T head2, T head3, Nel<T> tail) => new(head1, Seq.Cons(head2, head3, tail.ToSeq()));

    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, Nel<T> tail) => new(head1, Seq.Cons(head2, head3, head4, tail.ToSeq()));

    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, Nel<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, tail.ToSeq()));

    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, Nel<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, head6, tail.ToSeq()));

    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, T head7, Nel<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, head6, head7, tail.ToSeq()));

    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, T head7, T head8, Nel<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, head6, head7, head8, tail.ToSeq()));

    public static Nel<T> Concat<T>(Nel<T> first, Seq<T> second) => first.Append(second);

    public static Nel<T> Concat<T>(Nel<T> first, Nel<T> second) => first.Append(second);
}
