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

    public static void Use<T>(T disposable, Action<T> action) where T : IDisposable
    {
        using (disposable)
        {
            action(disposable);
        }
    }

    public static TResult Use<T, TResult>(T disposable, Func<T, TResult> fn) where T : IDisposable
    {
        using (disposable)
        {
            return fn(disposable);
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
}
