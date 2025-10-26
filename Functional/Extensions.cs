using System.Runtime.CompilerServices;

namespace Macaron.Functional;

public static partial class Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? TakeIf<T>(this T self, Func<T, bool> predicate) where T : class
    {
        return predicate(self) ? self : null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? TakeUnless<T>(this T self, Func<T, bool> predicate) where T : class
    {
        return !predicate(self) ? self : null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Use<T>(this T disposable, Action<T> action) where T : IDisposable
    {
        using (disposable)
        {
            action(disposable);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Use<T, TResult>(this T disposable, Func<T, TResult> fn) where T : IDisposable
    {
        using (disposable)
        {
            return fn(disposable);
        }
    }
}
