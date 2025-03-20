// <auto-generated />
namespace Macaron.Functional;

partial class Utility
{
    public static Func<T1, T2, TResult> Uncurry<T1, T2, TResult>(
        Func<T1, Func<T2, TResult>> fn
    )
    {
        return (arg1, arg2) => fn(arg1)(arg2);
    }

    public static Action<T1, T2> Uncurry<T1, T2>(
        Func<T1, Action<T2>> action
    )
    {
        return (arg1, arg2) => action(arg1)(arg2);
    }

    public static Func<T1, T2, T3, TResult> Uncurry<T1, T2, T3, TResult>(
        Func<T1, Func<T2, Func<T3, TResult>>> fn
    )
    {
        return (arg1, arg2, arg3) => fn(arg1)(arg2)(arg3);
    }

    public static Action<T1, T2, T3> Uncurry<T1, T2, T3>(
        Func<T1, Func<T2, Action<T3>>> action
    )
    {
        return (arg1, arg2, arg3) => action(arg1)(arg2)(arg3);
    }

    public static Func<T1, T2, T3, T4, TResult> Uncurry<T1, T2, T3, T4, TResult>(
        Func<T1, Func<T2, Func<T3, Func<T4, TResult>>>> fn
    )
    {
        return (arg1, arg2, arg3, arg4) => fn(arg1)(arg2)(arg3)(arg4);
    }

    public static Action<T1, T2, T3, T4> Uncurry<T1, T2, T3, T4>(
        Func<T1, Func<T2, Func<T3, Action<T4>>>> action
    )
    {
        return (arg1, arg2, arg3, arg4) => action(arg1)(arg2)(arg3)(arg4);
    }

    public static Func<T1, T2, T3, T4, T5, TResult> Uncurry<T1, T2, T3, T4, T5, TResult>(
        Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> fn
    )
    {
        return (arg1, arg2, arg3, arg4, arg5) => fn(arg1)(arg2)(arg3)(arg4)(arg5);
    }

    public static Action<T1, T2, T3, T4, T5> Uncurry<T1, T2, T3, T4, T5>(
        Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> action
    )
    {
        return (arg1, arg2, arg3, arg4, arg5) => action(arg1)(arg2)(arg3)(arg4)(arg5);
    }

    public static Func<T1, T2, T3, T4, T5, T6, TResult> Uncurry<T1, T2, T3, T4, T5, T6, TResult>(
        Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, TResult>>>>>> fn
    )
    {
        return (arg1, arg2, arg3, arg4, arg5, arg6) => fn(arg1)(arg2)(arg3)(arg4)(arg5)(arg6);
    }

    public static Action<T1, T2, T3, T4, T5, T6> Uncurry<T1, T2, T3, T4, T5, T6>(
        Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> action
    )
    {
        return (arg1, arg2, arg3, arg4, arg5, arg6) => action(arg1)(arg2)(arg3)(arg4)(arg5)(arg6);
    }

    public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> Uncurry<T1, T2, T3, T4, T5, T6, T7, TResult>(
        Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, TResult>>>>>>> fn
    )
    {
        return (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => fn(arg1)(arg2)(arg3)(arg4)(arg5)(arg6)(arg7);
    }

    public static Action<T1, T2, T3, T4, T5, T6, T7> Uncurry<T1, T2, T3, T4, T5, T6, T7>(
        Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> action
    )
    {
        return (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => action(arg1)(arg2)(arg3)(arg4)(arg5)(arg6)(arg7);
    }

    public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Uncurry<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
        Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, TResult>>>>>>>> fn
    )
    {
        return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => fn(arg1)(arg2)(arg3)(arg4)(arg5)(arg6)(arg7)(arg8);
    }

    public static Action<T1, T2, T3, T4, T5, T6, T7, T8> Uncurry<T1, T2, T3, T4, T5, T6, T7, T8>(
        Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> action
    )
    {
        return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => action(arg1)(arg2)(arg3)(arg4)(arg5)(arg6)(arg7)(arg8);
    }

}
