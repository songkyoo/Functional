using System.Runtime.CompilerServices;

namespace Macaron.Functional;

public static partial class Maybe
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<T> Just<T>(T value)
    {
        return new JustMaybe<T>(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NothingMaybe Nothing()
    {
        return new NothingMaybe();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<T> Nothing<T>()
    {
        return new NothingMaybe<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<T> Of<T>(T? value) where T : class
    {
        return value != null ? Just(value) : Nothing<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<T> Of<T>(T? value) where T : struct
    {
        return value != null ? Just(value.Value) : Nothing<T>();
    }
}
