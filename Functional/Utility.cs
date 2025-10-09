namespace Macaron.Functional;

public static partial class Utility
{
    public static void Run(Action action)
    {
        action();
    }

    public static TResult Run<TResult>(Func<TResult> fn)
    {
        return fn();
    }

    public static Either<Exception, Placeholder> RunCatching(Action action)
    {
        try
        {
            action();

            return Either.Right(Placeholder._);
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    public static Either<Exception, TResult> RunCatching<TResult>(Func<TResult> fn)
    {
        try
        {
            return Either.Right(fn());
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    public static void Run<T>(Action<T> action, T context)
    {
        action(context);
    }

    public static TResult Run<T, TResult>(Func<T, TResult> fn, T context)
    {
        return fn(context);
    }

    public static Either<Exception, Placeholder> RunCatching<T>(Action<T> action, T context)
    {
        try
        {
            action(context);

            return Either.Right(Placeholder._);
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    public static Either<Exception, TResult> RunCatching<T, TResult>(Func<T, TResult> fn, T context)
    {
        try
        {
            return Either.Right(fn(context));
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    public static Func<T, T> Identity<T>()
    {
        return static value => value;
    }

    public static T Identity<T>(T value)
    {
        return value;
    }

    public static Func<T> Constant<T>(T value)
    {
        return () => value;
    }

    public static Func<T, bool> Is<T>(T value)
    {
        return otherValue => EqualityComparer<T>.Default.Equals(otherValue, value);
    }

    public static Func<T, bool> Is<T>(T value, IEqualityComparer<T> comparer)
    {
        return otherValue => comparer.Equals(otherValue, value);
    }

    public static Func<T, bool> IsNot<T>(T value)
    {
        return otherValue => !EqualityComparer<T>.Default.Equals(otherValue, value);
    }

    public static Func<T, bool> IsNot<T>(T value, IEqualityComparer<T> comparer)
    {
        return otherValue => !comparer.Equals(otherValue, value);
    }

    public static Func<T?, bool> IsNull<T>()
    {
        return static value => value == null;
    }

    public static bool IsNull<T>(T? value)
    {
        return value == null;
    }

    public static Func<T?, bool> IsNotNull<T>()
    {
        return static value => value != null;
    }

    public static bool IsNotNull<T>(T? value)
    {
        return value != null;
    }

    public static TResult Then<T1, T2, TResult>(
        (T1, T2) tuple,
        Func<T1, T2, TResult> fn
    )
    {
        return fn(tuple.Item1, tuple.Item2);
    }

    public static TResult Then<T1, T2, T3, TResult>(
        (T1, T2, T3) tuple,
        Func<T1, T2, T3, TResult> fn
    )
    {
        return fn(tuple.Item1, tuple.Item2, tuple.Item3);
    }

    public static (TResult1, TResult2) Map<T1, T2, TResult1, TResult2>(
        (T1, T2) tuple,
        Func<T1, TResult1> f1,
        Func<T2, TResult2> f2
    )
    {
        return (f1(tuple.Item1), f2(tuple.Item2));
    }

    public static (TResult1, TResult2, TResult3) Map<T1, T2, T3, TResult1, TResult2, TResult3>(
        (T1, T2, T3) tuple,
        Func<T1, TResult1> f1,
        Func<T2, TResult2> f2,
        Func<T3, TResult3> f3
    )
    {
        return (f1(tuple.Item1), f2(tuple.Item2), f3(tuple.Item3));
    }

    public static (TResult1, T2) MapFirst<T1, T2, TResult1>(
        (T1, T2) tuple,
        Func<T1, TResult1> fn
    )
    {
        return (fn(tuple.Item1), tuple.Item2);
    }

    public static (T1, TResult2) MapSecond<T1, T2, TResult2>(
        (T1, T2) tuple,
        Func<T2, TResult2> fn
    )
    {
        return (tuple.Item1, fn(tuple.Item2));
    }

    public static (TResult1, T2, T3) MapFirst<T1, T2, T3, TResult1>(
        (T1, T2, T3) tuple,
        Func<T1, TResult1> fn
    )
    {
        return (fn(tuple.Item1), tuple.Item2, tuple.Item3);
    }

    public static (T1, TResult2, T3) MapSecond<T1, T2, T3, TResult2>(
        (T1, T2, T3) tuple,
        Func<T2, TResult2> fn
    )
    {
        return (tuple.Item1, fn(tuple.Item2), tuple.Item3);
    }

    public static (T1, T2, TResult3) MapThird<T1, T2, T3, TResult3>(
        (T1, T2, T3) tuple,
        Func<T3, TResult3> fn
    )
    {
        return (tuple.Item1, tuple.Item2, fn(tuple.Item3));
    }

    public static ((T1, T1), (T2, T2)) Zip<T1, T2>(
        (T1, T2) left,
        (T1, T2) right
    )
    {
        return ((left.Item1, right.Item1), (left.Item2, right.Item2));
    }

    public static ((T1, T1), (T2, T2), (T3, T3)) Zip<T1, T2, T3>(
        (T1, T2, T3) left,
        (T1, T2, T3) right
    )
    {
        return ((left.Item1, right.Item1), (left.Item2, right.Item2), (left.Item3, right.Item3));
    }

    public static (TResult1, TResult2) ZipWith<T1, T2, TResult1, TResult2>(
        (T1, T2) left,
        (T1, T2) right,
        Func<T1, T1, TResult1> f1,
        Func<T2, T2, TResult2> f2
    )
    {
        return (f1(left.Item1, right.Item1), f2(left.Item2, right.Item2));
    }

    public static (TResult1, TResult2, TResult3) ZipWith<T1, T2, T3, TResult1, TResult2, TResult3>(
        (T1, T2, T3) left,
        (T1, T2, T3) right,
        Func<T1, T1, TResult1> f1,
        Func<T2, T2, TResult2> f2,
        Func<T3, T3, TResult3> f3
    )
    {
        return (f1(left.Item1, right.Item1), f2(left.Item2, right.Item2), f3(left.Item3, right.Item3));
    }
}
