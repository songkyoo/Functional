namespace Macaron.Functional;

public static partial class Extensions
{
    public static T? TakeIf<T>(this T self, Func<T, bool> predicate) where T : class
    {
        return predicate(self) ? self : null;
    }

    public static T? TakeUnless<T>(this T self, Func<T, bool> predicate) where T : class
    {
        return !predicate(self) ? self : null;
    }

    public static TResult Use<T, TResult>(this T disposable, Func<T, TResult> fn) where T : IDisposable
    {
        using (disposable)
        {
            return fn(disposable);
        }
    }

    public static TResult Then<T1, T2, TResult>(
        this (T1, T2) tuple,
        Func<T1, T2, TResult> fn
    )
    {
        return fn(tuple.Item1, tuple.Item2);
    }

    public static TResult Then<T1, T2, T3, TResult>(
        this (T1, T2, T3) tuple,
        Func<T1, T2, T3, TResult> fn
    )
    {
        return fn(tuple.Item1, tuple.Item2, tuple.Item3);
    }


    public static (TResult1, TResult2) Map<T1, T2, TResult1, TResult2>(
        this (T1, T2) tuple,
        Func<T1, TResult1> f1,
        Func<T2, TResult2> f2
    )
    {
        return (f1(tuple.Item1), f2(tuple.Item2));
    }

    public static (TResult1, TResult2, TResult3) Map<T1, T2, T3, TResult1, TResult2, TResult3>(
        this (T1, T2, T3) tuple,
        Func<T1, TResult1> f1,
        Func<T2, TResult2> f2,
        Func<T3, TResult3> f3
    )
    {
        return (f1(tuple.Item1), f2(tuple.Item2), f3(tuple.Item3));
    }

    public static ((T1, T1), (T2, T2)) Zip<T1, T2>(
        this (T1, T2) tuple,
        (T1, T2) other
    )
    {
        return ((tuple.Item1, other.Item1), (tuple.Item2, other.Item2));
    }

    public static ((T1, T1), (T2, T2), (T3, T3)) Zip<T1, T2, T3>(
        this (T1, T2, T3) tuple,
        (T1, T2, T3) other
    )
    {
        return ((tuple.Item1, other.Item1), (tuple.Item2, other.Item2), (tuple.Item3, other.Item3));
    }

    public static (TResult1, TResult2) ZipWith<T1, T2, TResult1, TResult2>(
        this (T1, T2) tuple,
        (T1, T2) other,
        Func<T1, T1, TResult1> f1,
        Func<T2, T2, TResult2> f2
    )
    {
        return (f1(tuple.Item1, other.Item1), f2(tuple.Item2, other.Item2));
    }

    public static (TResult1, TResult2, TResult3) ZipWith<T1, T2, T3, TResult1, TResult2, TResult3>(
        this (T1, T2, T3) tuple,
        (T1, T2, T3) other,
        Func<T1, T1, TResult1> f1,
        Func<T2, T2, TResult2> f2,
        Func<T3, T3, TResult3> f3
    )
    {
        return (f1(tuple.Item1, other.Item1), f2(tuple.Item2, other.Item2), f3(tuple.Item3, other.Item3));
    }
}
