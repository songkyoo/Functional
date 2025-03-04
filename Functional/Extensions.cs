namespace Macaron.Functional;

public static partial class Extensions
{
    public static TResult Let<T, TResult>(this T self, Func<T, TResult> fn) where T : notnull
    {
        return fn(self);
    }

    public static T Also<T>(this T self, Action<T> action) where T : notnull
    {
        action(self);
        return self;
    }

    public static T? TakeIf<T>(this T self, Func<T, bool> predicate) where T : class
    {
        return predicate(self) ? self : null;
    }

    public static T? TakeIfStruct<T>(this T self, Func<T, bool> predicate) where T : struct
    {
        return predicate(self) ? self : null;
    }

    public static T? TakeUnless<T>(this T self, Func<T, bool> predicate) where T : class
    {
        return !predicate(self) ? self : null;
    }

    public static T? TakeUnlessStruct<T>(this T self, Func<T, bool> predicate) where T : struct
    {
        return !predicate(self) ? self : null;
    }

    public static TResult Use<T, TResult>(this T disposable, Func<T, TResult> fn) where T : IDisposable
    {
        using (disposable)
        {
            return fn(disposable);
        }
    }
}
