using System.Runtime.CompilerServices;

namespace Macaron.Functional;

partial class Utility
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Use<T>(
        T disposable,
        Action<T> action
    )
        where T : IDisposable
    {
        using (disposable)
        {
            action(disposable);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Use<T1, T2>(
        T1 disposable1,
        T2 disposable2,
        Action<T1, T2> action
    )
        where T1 : IDisposable
        where T2 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        {
            action(disposable1, disposable2);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Use<T1, T2, T3>(
        T1 disposable1,
        T2 disposable2,
        T3 disposable3,
        Action<T1, T2, T3> action
    )
        where T1 : IDisposable
        where T2 : IDisposable
        where T3 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        using (disposable3)
        {
            action(disposable1, disposable2, disposable3);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Use<T1, T2, T3, T4>(
        T1 disposable1,
        T2 disposable2,
        T3 disposable3,
        T4 disposable4,
        Action<T1, T2, T3, T4> action
    )
        where T1 : IDisposable
        where T2 : IDisposable
        where T3 : IDisposable
        where T4 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        using (disposable3)
        using (disposable4)
        {
            action(disposable1, disposable2, disposable3, disposable4);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Use<T1, T2, T3, T4, T5>(
        T1 disposable1,
        T2 disposable2,
        T3 disposable3,
        T4 disposable4,
        T5 disposable5,
        Action<T1, T2, T3, T4, T5> action
    )
        where T1 : IDisposable
        where T2 : IDisposable
        where T3 : IDisposable
        where T4 : IDisposable
        where T5 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        using (disposable3)
        using (disposable4)
        using (disposable5)
        {
            action(disposable1, disposable2, disposable3, disposable4, disposable5);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Use<T1, T2, T3, T4, T5, T6>(
        T1 disposable1,
        T2 disposable2,
        T3 disposable3,
        T4 disposable4,
        T5 disposable5,
        T6 disposable6,
        Action<T1, T2, T3, T4, T5, T6> action
    )
        where T1 : IDisposable
        where T2 : IDisposable
        where T3 : IDisposable
        where T4 : IDisposable
        where T5 : IDisposable
        where T6 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        using (disposable3)
        using (disposable4)
        using (disposable5)
        using (disposable6)
        {
            action(disposable1, disposable2, disposable3, disposable4, disposable5, disposable6);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Use<T1, T2, T3, T4, T5, T6, T7>(
        T1 disposable1,
        T2 disposable2,
        T3 disposable3,
        T4 disposable4,
        T5 disposable5,
        T6 disposable6,
        T7 disposable7,
        Action<T1, T2, T3, T4, T5, T6, T7> action
    )
        where T1 : IDisposable
        where T2 : IDisposable
        where T3 : IDisposable
        where T4 : IDisposable
        where T5 : IDisposable
        where T6 : IDisposable
        where T7 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        using (disposable3)
        using (disposable4)
        using (disposable5)
        using (disposable6)
        using (disposable7)
        {
            action(disposable1, disposable2, disposable3, disposable4, disposable5, disposable6, disposable7);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Use<T1, T2, T3, T4, T5, T6, T7, T8>(
        T1 disposable1,
        T2 disposable2,
        T3 disposable3,
        T4 disposable4,
        T5 disposable5,
        T6 disposable6,
        T7 disposable7,
        T8 disposable8,
        Action<T1, T2, T3, T4, T5, T6, T7, T8> action
    )
        where T1 : IDisposable
        where T2 : IDisposable
        where T3 : IDisposable
        where T4 : IDisposable
        where T5 : IDisposable
        where T6 : IDisposable
        where T7 : IDisposable
        where T8 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        using (disposable3)
        using (disposable4)
        using (disposable5)
        using (disposable6)
        using (disposable7)
        using (disposable8)
        {
            action(
                disposable1,
                disposable2,
                disposable3,
                disposable4,
                disposable5,
                disposable6,
                disposable7,
                disposable8
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Use<T, TResult>(
        T disposable,
        Func<T, TResult> fn
    )
        where T : IDisposable
    {
        using (disposable)
        {
            return fn(disposable);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Use<T1, T2, TResult>(
        T1 disposable1,
        T2 disposable2,
        Func<T1, T2, TResult> fn
    )
        where T1 : IDisposable
        where T2 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        {
            return fn(disposable1, disposable2);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Use<T1, T2, T3, TResult>(
        T1 disposable1,
        T2 disposable2,
        T3 disposable3,
        Func<T1, T2, T3, TResult> fn
    )
        where T1 : IDisposable
        where T2 : IDisposable
        where T3 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        using (disposable3)
        {
            return fn(disposable1, disposable2, disposable3);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Use<T1, T2, T3, T4, TResult>(
        T1 disposable1,
        T2 disposable2,
        T3 disposable3,
        T4 disposable4,
        Func<T1, T2, T3, T4, TResult> fn
    )
        where T1 : IDisposable
        where T2 : IDisposable
        where T3 : IDisposable
        where T4 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        using (disposable3)
        using (disposable4)
        {
            return fn(disposable1, disposable2, disposable3, disposable4);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Use<T1, T2, T3, T4, T5, TResult>(
        T1 disposable1,
        T2 disposable2,
        T3 disposable3,
        T4 disposable4,
        T5 disposable5,
        Func<T1, T2, T3, T4, T5, TResult> fn
    )
        where T1 : IDisposable
        where T2 : IDisposable
        where T3 : IDisposable
        where T4 : IDisposable
        where T5 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        using (disposable3)
        using (disposable4)
        using (disposable5)
        {
            return fn(disposable1, disposable2, disposable3, disposable4, disposable5);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Use<T1, T2, T3, T4, T5, T6, TResult>(
        T1 disposable1,
        T2 disposable2,
        T3 disposable3,
        T4 disposable4,
        T5 disposable5,
        T6 disposable6,
        Func<T1, T2, T3, T4, T5, T6, TResult> fn
    )
        where T1 : IDisposable
        where T2 : IDisposable
        where T3 : IDisposable
        where T4 : IDisposable
        where T5 : IDisposable
        where T6 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        using (disposable3)
        using (disposable4)
        using (disposable5)
        using (disposable6)
        {
            return fn(disposable1, disposable2, disposable3, disposable4, disposable5, disposable6);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Use<T1, T2, T3, T4, T5, T6, T7, TResult>(
        T1 disposable1,
        T2 disposable2,
        T3 disposable3,
        T4 disposable4,
        T5 disposable5,
        T6 disposable6,
        T7 disposable7,
        Func<T1, T2, T3, T4, T5, T6, T7, TResult> fn
    )
        where T1 : IDisposable
        where T2 : IDisposable
        where T3 : IDisposable
        where T4 : IDisposable
        where T5 : IDisposable
        where T6 : IDisposable
        where T7 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        using (disposable3)
        using (disposable4)
        using (disposable5)
        using (disposable6)
        using (disposable7)
        {
            return fn(disposable1, disposable2, disposable3, disposable4, disposable5, disposable6, disposable7);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Use<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
        T1 disposable1,
        T2 disposable2,
        T3 disposable3,
        T4 disposable4,
        T5 disposable5,
        T6 disposable6,
        T7 disposable7,
        T8 disposable8,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> fn
    )
        where T1 : IDisposable
        where T2 : IDisposable
        where T3 : IDisposable
        where T4 : IDisposable
        where T5 : IDisposable
        where T6 : IDisposable
        where T7 : IDisposable
        where T8 : IDisposable
    {
        using (disposable1)
        using (disposable2)
        using (disposable3)
        using (disposable4)
        using (disposable5)
        using (disposable6)
        using (disposable7)
        using (disposable8)
        {
            return fn(
                disposable1,
                disposable2,
                disposable3,
                disposable4,
                disposable5,
                disposable6,
                disposable7,
                disposable8
            );
        }
    }
}
