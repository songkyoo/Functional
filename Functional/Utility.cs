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

    public static T Identity<T>(T value)
    {
        return value;
    }

    public static Func<T> Constant<T>(T value)
    {
        return () => value;
    }

    public static Action<T2, T1> Flip<T1, T2>(Action<T1, T2> action)
    {
        return (t2, t1) => action(t1, t2);
    }

    public static Func<T2, T1, TResult> Flip<T1, T2, TResult>(Func<T1, T2, TResult> fn)
    {
        return (t2, t1) => fn(t1, t2);
    }
}
