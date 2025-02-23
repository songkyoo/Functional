namespace Macaron.Functional;

public static class Extensions
{
    public static TResult Let<T, TResult>(this T obj, Func<T, TResult> fn) where T : notnull
    {
        return fn(obj);
    }

    public static TResult Let<T, TContext, TResult>(this T obj, in TContext context, Func<T, TContext, TResult> fn)
        where T : notnull
    {
        return fn(obj, context);
    }

    public static T Also<T>(this T obj, Action<T> action) where T : notnull
    {
        action(obj);
        return obj;
    }

    public static T Also<T, TContext>(this T obj, in TContext context, Action<T, TContext> action) where T : notnull
    {
        action(obj, context);
        return obj;
    }

    public static T? TakeIf<T>(this T self, Func<T, bool> predicate) where T : class
    {
        return predicate(self) ? self : null;
    }

    public static T? TakeIf<T, TContext>(this T self, TContext context, Func<T, TContext, bool> predicate)
        where T : class
    {
        return predicate(self, context) ? self : null;
    }

    public static T? TakeIfStruct<T>(this T self, Func<T, bool> predicate) where T : struct
    {
        return predicate(self) ? self : null;
    }

    public static T? TakeIfStruct<T, TContext>(this T self, in TContext context, Func<T, TContext, bool> predicate)
        where T : struct
    {
        return predicate(self, context) ? self : null;
    }

    public static T? TakeUnless<T>(this T self, Func<T, bool> predicate) where T : class
    {
        return !predicate(self) ? self : null;
    }

    public static T? TakeUnless<T, TContext>(this T self, in TContext context, Func<T, TContext, bool> predicate)
        where T : class
    {
        return !predicate(self, context) ? self : null;
    }

    public static T? TakeUnlessStruct<T>(this T self, Func<T, bool> predicate) where T : struct
    {
        return !predicate(self) ? self : null;
    }

    public static T? TakeUnlessStruct<T, TContext>(this T self, in TContext context, Func<T, TContext, bool> predicate)
        where T : struct
    {
        return !predicate(self, context) ? self : null;
    }

    public static TResult Use<T, TResult>(this T disposable, Func<T, TResult> fn) where T : IDisposable
    {
        using (disposable)
        {
            return fn(disposable);
        }
    }
}
