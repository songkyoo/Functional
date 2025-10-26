using System.Runtime.CompilerServices;

namespace Macaron.Functional;

public static partial class Utility
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Run(Action action)
    {
        action();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Run<TResult>(Func<TResult> fn)
    {
        return fn();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Run<T>(T value, Action<T> action)
    {
        action(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Run<T, TResult>(T value, Func<T, TResult> fn)
    {
        return fn(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, Placeholder> RunCatching<T>(T value, Action<T> action)
    {
        try
        {
            action(value);

            return Either.Right(Placeholder._);
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, TResult> RunCatching<T, TResult>(T value, Func<T, TResult> fn)
    {
        try
        {
            return Either.Right(fn(value));
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Use<T>(T disposable, Action<T> action) where T : IDisposable
    {
        using (disposable)
        {
            action(disposable);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T> Constant<T>(T value)
    {
        return () => value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T, bool> Is<T>(T value)
    {
        return otherValue => EqualityComparer<T>.Default.Equals(otherValue, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T, bool> Is<T>(T value, IEqualityComparer<T> comparer)
    {
        return otherValue => comparer.Equals(otherValue, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T, bool> IsNot<T>(T value)
    {
        return otherValue => !EqualityComparer<T>.Default.Equals(otherValue, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T, bool> IsNot<T>(T value, IEqualityComparer<T> comparer)
    {
        return otherValue => !comparer.Equals(otherValue, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T?, bool> IsNull<T>()
    {
        return static value => value == null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNull<T>(T? value)
    {
        return value == null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T?, bool> IsNotNull<T>()
    {
        return static value => value != null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNull<T>(T? value)
    {
        return value != null;
    }
}
