using System.Runtime.CompilerServices;

namespace Macaron.Functional;

public static class Seq
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Of<T>(T value) => new Seq<T>.Node(value, Seq<T>.Nil);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Of<T>(T value1, T value2) => new Seq<T>.Node(value1, new Seq<T>.Node(value2, Seq<T>.Nil));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Of<T>(T value1, T value2, T value3) => new Seq<T>.Node(value1, new Seq<T>.Node(value2, new Seq<T>.Node(value3, Seq<T>.Nil)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Of<T>(T value1, T value2, T value3, T value4) => new Seq<T>.Node(value1, new Seq<T>.Node(value2, new Seq<T>.Node(value3, new Seq<T>.Node(value4, Seq<T>.Nil))));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Of<T>(T value1, T value2, T value3, T value4, T value5) => new Seq<T>.Node(value1, new Seq<T>.Node(value2, new Seq<T>.Node(value3, new Seq<T>.Node(value4, new Seq<T>.Node(value5, Seq<T>.Nil)))));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Of<T>(T value1, T value2, T value3, T value4, T value5, T value6) => new Seq<T>.Node(value1, new Seq<T>.Node(value2, new Seq<T>.Node(value3, new Seq<T>.Node(value4, new Seq<T>.Node(value5, new Seq<T>.Node(value6, Seq<T>.Nil))))));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Of<T>(T value1, T value2, T value3, T value4, T value5, T value6, T value7) => new Seq<T>.Node(value1, new Seq<T>.Node(value2, new Seq<T>.Node(value3, new Seq<T>.Node(value4, new Seq<T>.Node(value5, new Seq<T>.Node(value6, new Seq<T>.Node(value7, Seq<T>.Nil)))))));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Of<T>(T value1, T value2, T value3, T value4, T value5, T value6, T value7, T value8) => new Seq<T>.Node(value1, new Seq<T>.Node(value2, new Seq<T>.Node(value3, new Seq<T>.Node(value4, new Seq<T>.Node(value5, new Seq<T>.Node(value6, new Seq<T>.Node(value7, new Seq<T>.Node(value8, Seq<T>.Nil))))))));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Of<T>(params T[] values)
    {
        if (values == null)
        {
            throw new ArgumentNullException(nameof(values));
        }

        var result = Seq<T>.Nil;

        for (var i = values.Length - 1; i >= 0; i--)
        {
            result = new Seq<T>.Node(values[i], result);
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Cons<T>(T head, Seq<T> tail) => new Seq<T>.Node(head, tail);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Cons<T>(T head1, T head2, Seq<T> tail) => new Seq<T>.Node(head1, new Seq<T>.Node(head2, tail));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Cons<T>(T head1, T head2, T head3, Seq<T> tail) => new Seq<T>.Node(head1, new Seq<T>.Node(head2, new Seq<T>.Node(head3, tail)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Cons<T>(T head1, T head2, T head3, T head4, Seq<T> tail) => new Seq<T>.Node(head1, new Seq<T>.Node(head2, new Seq<T>.Node(head3, new Seq<T>.Node(head4, tail))));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, Seq<T> tail) => new Seq<T>.Node(head1, new Seq<T>.Node(head2, new Seq<T>.Node(head3, new Seq<T>.Node(head4, new Seq<T>.Node(head5, tail)))));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, Seq<T> tail) => new Seq<T>.Node(head1, new Seq<T>.Node(head2, new Seq<T>.Node(head3, new Seq<T>.Node(head4, new Seq<T>.Node(head5, new Seq<T>.Node(head6, tail))))));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, T head7, Seq<T> tail) => new Seq<T>.Node(head1, new Seq<T>.Node(head2, new Seq<T>.Node(head3, new Seq<T>.Node(head4, new Seq<T>.Node(head5, new Seq<T>.Node(head6, new Seq<T>.Node(head7, tail)))))));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Cons<T>(T head1, T head2, T head3, T head4, T head5, T head6, T head7, T head8, Seq<T> tail) => new Seq<T>.Node(head1, new Seq<T>.Node(head2, new Seq<T>.Node(head3, new Seq<T>.Node(head4, new Seq<T>.Node(head5, new Seq<T>.Node(head6, new Seq<T>.Node(head7, new Seq<T>.Node(head8, tail))))))));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Seq<T> Concat<T>(Seq<T> first, Seq<T> second) => first.Concat(second);
}
