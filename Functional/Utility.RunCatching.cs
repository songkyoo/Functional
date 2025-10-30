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
}
