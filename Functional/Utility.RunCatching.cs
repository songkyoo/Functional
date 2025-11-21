using System.Runtime.CompilerServices;

namespace Macaron.Functional;

partial class Utility
{
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
    public static Either<Exception, Placeholder> RunCatching<T1, T2>(
        (T1, T2) value,
        Action<T1, T2> action
    )
    {
        try
        {
            action(value.Item1, value.Item2);

            return Either.Right(Placeholder._);
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, Placeholder> RunCatching<T1, T2, T3>(
        (T1, T2, T3) value,
        Action<T1, T2, T3> action
    )
    {
        try
        {
            action(value.Item1, value.Item2, value.Item3);

            return Either.Right(Placeholder._);
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, Placeholder> RunCatching<T1, T2, T3, T4>(
        (T1, T2, T3, T4) value,
        Action<T1, T2, T3, T4> action
    )
    {
        try
        {
            action(value.Item1, value.Item2, value.Item3, value.Item4);

            return Either.Right(Placeholder._);
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, Placeholder> RunCatching<T1, T2, T3, T4, T5>(
        (T1, T2, T3, T4, T5) value,
        Action<T1, T2, T3, T4, T5> action
    )
    {
        try
        {
            action(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);

            return Either.Right(Placeholder._);
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, Placeholder> RunCatching<T1, T2, T3, T4, T5, T6>(
        (T1, T2, T3, T4, T5, T6) value,
        Action<T1, T2, T3, T4, T5, T6> action
    )
    {
        try
        {
            action(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);

            return Either.Right(Placeholder._);
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, Placeholder> RunCatching<T1, T2, T3, T4, T5, T6, T7>(
        (T1, T2, T3, T4, T5, T6, T7) value,
        Action<T1, T2, T3, T4, T5, T6, T7> action
    )
    {
        try
        {
            action(
                value.Item1,
                value.Item2,
                value.Item3,
                value.Item4,
                value.Item5,
                value.Item6,
                value.Item7
            );

            return Either.Right(Placeholder._);
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, Placeholder> RunCatching<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7,
        T8
    >(
        (T1, T2, T3, T4, T5, T6, T7, T8) value,
        Action<T1, T2, T3, T4, T5, T6, T7, T8> action
    )
    {
        try
        {
            action(
                value.Item1,
                value.Item2,
                value.Item3,
                value.Item4,
                value.Item5,
                value.Item6,
                value.Item7,
                value.Item8
            );

            return Either.Right(Placeholder._);
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, TResult> RunCatching<T1, T2, TResult>(
        (T1, T2) value,
        Func<T1, T2, TResult> fn
    )
    {
        try
        {
            return Either.Right(fn(value.Item1, value.Item2));
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, TResult> RunCatching<T1, T2, T3, TResult>(
        (T1, T2, T3) value,
        Func<T1, T2, T3, TResult> fn
    )
    {
        try
        {
            return Either.Right(fn(value.Item1, value.Item2, value.Item3));
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, TResult> RunCatching<T1, T2, T3, T4, TResult>(
        (T1, T2, T3, T4) value,
        Func<T1, T2, T3, T4, TResult> fn
    )
    {
        try
        {
            return Either.Right(fn(value.Item1, value.Item2, value.Item3, value.Item4));
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, TResult> RunCatching<T1, T2, T3, T4, T5, TResult>(
        (T1, T2, T3, T4, T5) value,
        Func<T1, T2, T3, T4, T5, TResult> fn
    )
    {
        try
        {
            return Either.Right(fn(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5));
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, TResult> RunCatching<T1, T2, T3, T4, T5, T6, TResult>(
        (T1, T2, T3, T4, T5, T6) value,
        Func<T1, T2, T3, T4, T5, T6, TResult> fn
    )
    {
        try
        {
            return Either.Right(
                fn(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6)
            );
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, TResult> RunCatching<T1, T2, T3, T4, T5, T6, T7, TResult>(
        (T1, T2, T3, T4, T5, T6, T7) value,
        Func<T1, T2, T3, T4, T5, T6, T7, TResult> fn
    )
    {
        try
        {
            return Either.Right(
                fn(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7)
            );
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<Exception, TResult> RunCatching<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7,
        T8,
        TResult
    >(
        (T1, T2, T3, T4, T5, T6, T7, T8) value,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> fn
    )
    {
        try
        {
            return Either.Right(
                fn(
                    value.Item1,
                    value.Item2,
                    value.Item3,
                    value.Item4,
                    value.Item5,
                    value.Item6,
                    value.Item7,
                    value.Item8
                )
            );
        }
        catch (Exception exception)
        {
            return Either.Left(exception);
        }
    }
}
