using System.Runtime.CompilerServices;

namespace Macaron.Functional;

public static partial class Utility
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T, T> Identity<T>()
    {
        return static value => value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
