using System.Runtime.CompilerServices;

namespace Macaron.Functional;

partial class Extensions
{
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
