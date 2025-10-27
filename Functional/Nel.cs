using System.Runtime.CompilerServices;

namespace Macaron.Functional;

public static class Nel
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Of<T>(T value) => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Of<T>(T value1, T value2) => new(value1, Seq.Of(value2));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Of<T>(T value1, T value2, T value3) => new(value1, Seq.Of(value2, value3));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Of<T>(T value1, T value2, T value3, T value4) => new(value1, Seq.Of(value2, value3, value4));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Of<T>(T value1, T value2, T value3, T value4, T value5) => new(value1, Seq.Of(value2, value3, value4, value5));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Of<T>(T value1, T value2, T value3, T value4, T value5, T value6) => new(value1, Seq.Of(value2, value3, value4, value5, value6));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Of<T>(T value1, T value2, T value3, T value4, T value5, T value6, T value7) => new(value1, Seq.Of(value2, value3, value4, value5, value6, value7));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Of<T>(T value1, T value2, T value3, T value4, T value5, T value6, T value7, T value8) => new(value1, Seq.Of(value2, value3, value4, value5, value6, value7, value8));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Of<T>(T value, params T[] values)
    {
        return values == null ? throw new ArgumentNullException(nameof(values)) : new Nel<T>(value, Seq.Of(values));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head, Seq<T> tail) => new(head, tail);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, Seq<T> tail) => new(head1, Seq.Cons(head2, tail));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, T head3, Seq<T> tail) => new(head1, Seq.Cons(head2, head3, tail));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, Seq<T> tail) => new(head1, Seq.Cons(head2, head3, head4, tail));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, Seq<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, tail));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, Seq<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, head6, tail));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, T head7, Seq<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, head6, head7, tail));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, T head7, T head8, Seq<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, head6, head7, head8, tail));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head, Nel<T> tail) => new(head, tail);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, Nel<T> tail) => new(head1, Seq.Cons(head2, tail.ToSeq()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, T head3, Nel<T> tail) => new(head1, Seq.Cons(head2, head3, tail.ToSeq()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, Nel<T> tail) => new(head1, Seq.Cons(head2, head3, head4, tail.ToSeq()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, Nel<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, tail.ToSeq()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, Nel<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, head6, tail.ToSeq()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, T head7, Nel<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, head6, head7, tail.ToSeq()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, T head7, T head8, Nel<T> tail) => new(head1, Seq.Cons(head2, head3, head4, head5, head6, head7, head8, tail.ToSeq()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Concat<T>(Nel<T> first, Seq<T> second) => first.Concat(second);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Nel<T> Concat<T>(Nel<T> first, Nel<T> second) => first.Concat(second);
}
