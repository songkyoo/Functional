namespace Macaron.Functional;

public static class Seq
{
    public static Seq<T> Of<T>(T value) => new(value);

    public static Seq<T> Of<T>(T value1, T value2)
    {
        return Cons(value1, new Seq<T>(value2));
    }

    public static Seq<T> Of<T>(T value1, T value2, T value3)
    {
        return Cons(value1, value2, new Seq<T>(value3));
    }

    public static Seq<T> Of<T>(T value1, T value2, T value3, T value4)
    {
        return Cons(value1, value2, value3, new Seq<T>(value4));
    }

    public static Seq<T> Of<T>(T value1, T value2, T value3, T value4, T value5)
    {
        return Cons(value1, value2, value3, value4, new Seq<T>(value5));
    }

    public static Seq<T> Of<T>(T value1, T value2, T value3, T value4, T value5, T value6)
    {
        return Cons(value1, value2, value3, value4, value5, new Seq<T>(value6));
    }

    public static Seq<T> Of<T>(T value1, T value2, T value3, T value4, T value5, T value6, T value7)
    {
        return Cons(value1, value2, value3, value4, value5, value6, new Seq<T>(value7));
    }

    public static Seq<T> Of<T>(T value1, T value2, T value3, T value4, T value5, T value6, T value7, T value8)
    {
        return Cons(value1, value2, value3, value4, value5, value6, value7, new Seq<T>(value8));
    }

    public static Seq<T> Of<T>(T value1, T value2, T value3, T value4, T value5, T value6, T value7, T value8, params T[] rest)
    {
        Seq<T>? tail = null;

        for (var i = rest.Length - 1; i >= 0; i--)
        {
            tail = new Seq<T>(rest[i], tail);
        }

        return tail != null ?
            Cons(value1, value2, value3, value4, value5, value6, value7, value8, tail) :
            Cons(value1, value2, value3, value4, value5, value6, value7, new Seq<T>(value8));
    }

    public static Seq<T> Cons<T>(T value, Seq<T> seq)
    {
        return new Seq<T>(value, seq);
    }

    public static Seq<T> Cons<T>(T value1, T value2, Seq<T> seq)
    {
        return new Seq<T>(value1, new Seq<T>(value2, seq));
    }

    public static Seq<T> Cons<T>(T value1, T value2, T value3, Seq<T> seq)
    {
        return new Seq<T>(value1, new Seq<T>(value2, new Seq<T>(value3, seq)));
    }

    public static Seq<T> Cons<T>(T value1, T value2, T value3, T value4, Seq<T> seq)
    {
        return new Seq<T>(value1, new Seq<T>(value2, new Seq<T>(value3, new Seq<T>(value4, seq))));
    }

    public static Seq<T> Cons<T>(T value1, T value2, T value3, T value4, T value5, Seq<T> seq)
    {
        return new Seq<T>(value1, new Seq<T>(value2, new Seq<T>(value3, new Seq<T>(value4, new Seq<T>(value5, seq)))));
    }

    public static Seq<T> Cons<T>(T value1, T value2, T value3, T value4, T value5, T value6, Seq<T> seq)
    {
        return new Seq<T>(value1, new Seq<T>(value2, new Seq<T>(value3, new Seq<T>(value4, new Seq<T>(value5, new Seq<T>(value6, seq))))));
    }

    public static Seq<T> Cons<T>(T value1, T value2, T value3, T value4, T value5, T value6, T value7, Seq<T> seq)
    {
        return new Seq<T>(value1, new Seq<T>(value2, new Seq<T>(value3, new Seq<T>(value4, new Seq<T>(value5, new Seq<T>(value6, new Seq<T>(value7, seq)))))));
    }

    public static Seq<T> Cons<T>(T value1, T value2, T value3, T value4, T value5, T value6, T value7, T value8, Seq<T> seq)
    {
        return new Seq<T>(value1, new Seq<T>(value2, new Seq<T>(value3, new Seq<T>(value4, new Seq<T>(value5, new Seq<T>(value6, new Seq<T>(value7, new Seq<T>(value8, seq))))))));
    }

    public static Seq<T> Concat<T>(Seq<T> first, Seq<T> second)
    {
        return first.Append(second);
    }
}
