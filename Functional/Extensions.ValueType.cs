using System.Runtime.CompilerServices;

namespace Macaron.Functional;

public static class ValueTypeExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? TakeIf<T>(this T self, Func<T, bool> predicate) where T : struct
    {
        return predicate(self) ? self : null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? TakeUnless<T>(this T self, Func<T, bool> predicate) where T : struct
    {
        return !predicate(self) ? self : null;
    }
}
