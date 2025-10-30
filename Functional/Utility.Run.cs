using System.Runtime.CompilerServices;

namespace Macaron.Functional;

partial class Utility
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
    public static void Run<T>(T value, Action<T> action)
    {
        action(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Run<T, TResult>(T value, Func<T, TResult> fn)
    {
        return fn(value);
    }
}
