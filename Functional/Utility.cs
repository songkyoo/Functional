namespace Macaron.Functional;

public static partial class Utility
{
    public static void Run<T>(T value, Action<T> action)
    {
        action.Invoke(value);
    }

    public static TResult Run<T, TResult>(T value, Func<T, TResult> fn)
    {
        return fn.Invoke(value);
    }

    public static Either<Exception, Placeholder> RunCatching<T>(T value, Action<T> fn)
    {
        try
        {
            fn.Invoke(value);
            return Either.Right(Placeholder._);
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    public static Either<TException, Placeholder> RunCatching<TException, T>(T value, Action<T> fn)
        where TException : Exception
    {
        try
        {
            fn.Invoke(value);
            return Either.Right(Placeholder._);
        }
        catch (TException exception)
        {
            return Either.Left(exception);
        }
    }

    public static Either<Exception, TResult> RunCatching<T, TResult>(T value, Func<T, TResult> fn)
    {
        try
        {
            return Either.Right(fn.Invoke(value));
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    public static Either<TException, TResult> RunCatching<TException, T, TResult>(T value, Func<T, TResult> fn)
        where TException : Exception
    {
        try
        {
            return Either.Right(fn.Invoke(value));
        }
        catch (TException exception)
        {
            return Either.Left(exception);
        }
    }

    public static T1 First<T1, T2>((T1, T2) tuple) => tuple.Item1;

    public static TKey First<TKey, TValue>(KeyValuePair<TKey, TValue> pair) => pair.Key;

    public static TKey Key<TKey, TValue>(KeyValuePair<TKey, TValue> pair) => pair.Key;

    public static T2 Second<T1, T2>((T1, T2) tuple) => tuple.Item2;

    public static TValue Second<TKey, TValue>(KeyValuePair<TKey, TValue> pair) => pair.Value;

    public static TValue Value<TKey, TValue>(KeyValuePair<TKey, TValue> pair) => pair.Value;

    public static Func<T, T> Identity<T>()
    {
        return static value => value;
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
        return value => value == null;
    }

    public static Func<T?, bool> IsNotNull<T>()
    {
        return value => value != null;
    }
}
