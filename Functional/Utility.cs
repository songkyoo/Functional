namespace Macaron.Functional;

public static partial class Utility
{
    public static T Identity<T>(T value)
    {
        return value;
    }

    public static Func<T> Constant<T>(T value)
    {
        return () => value;
    }

    public static Action<T2, T1> Flip<T1, T2>(Action<T1, T2> action)
    {
        return (t2, t1) => action(t1, t2);
    }

    public static Func<T2, T1, TResult> Flip<T1, T2, TResult>(Func<T1, T2, TResult> fn)
    {
        return (t2, t1) => fn(t1, t2);
    }
}
