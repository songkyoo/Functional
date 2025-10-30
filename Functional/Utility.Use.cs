using System.Runtime.CompilerServices;

namespace Macaron.Functional;

partial class Utility
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Use<T>(T disposable, Action<T> action) where T : IDisposable
    {
        using (disposable)
        {
            action(disposable);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Use<T1, T2>(T1 disposable1, T2 disposable2, Action<T1, T2> action) where T1 : IDisposable where T2 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        {
            action(disposable1, disposable2);
        }
    }

    // TODO 확장
    // public static void Use>(Action<DisposableBag> action) where T1 : IDisposable where T2 : IDisposable
    // {
    //     using (disposable2)
    //     {
    //         action(disposable1, disposable2);
    //     }
    // }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Use<T, TResult>(T disposable, Func<T, TResult> fn) where T : IDisposable
    {
        using (disposable)
        {
            return fn(disposable);
        }
    }

    // TODO 확장
}
