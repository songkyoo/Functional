namespace Macaron.Functional;

public static partial class Extensions
{
    public static TResult Let<T, TResult>(this T self, Func<T, TResult> fn) where T : notnull
    {
        return fn(self);
    }

    public static TResult Let<T, TContext, TResult>(this T self, in TContext context, Func<TContext, T, TResult> fn)
        where T : notnull
    {
        return fn(context, self);
    }

    public static T Also<T>(this T self, Action<T> action) where T : notnull
    {
        action(self);
        return self;
    }

    public static T Also<T, TContext>(this T self, in TContext context, Action<TContext, T> action) where T : notnull
    {
        action(context, self);
        return self;
    }

    public static T? TakeIf<T>(this T self, Func<T, bool> predicate) where T : class
    {
        return predicate(self) ? self : null;
    }

    public static T? TakeIf<T, TContext>(this T self, TContext context, Func<TContext, T, bool> predicate)
        where T : class
    {
        return predicate(context, self) ? self : null;
    }

    public static T? TakeIfStruct<T>(this T self, Func<T, bool> predicate) where T : struct
    {
        return predicate(self) ? self : null;
    }

    public static T? TakeIfStruct<T, TContext>(this T self, in TContext context, Func<TContext, T, bool> predicate)
        where T : struct
    {
        return predicate(context, self) ? self : null;
    }

    public static T? TakeUnless<T>(this T self, Func<T, bool> predicate) where T : class
    {
        return !predicate(self) ? self : null;
    }

    public static T? TakeUnless<T, TContext>(this T self, in TContext context, Func<TContext, T, bool> predicate)
        where T : class
    {
        return !predicate(context, self) ? self : null;
    }

    public static T? TakeUnlessStruct<T>(this T self, Func<T, bool> predicate) where T : struct
    {
        return !predicate(self) ? self : null;
    }

    public static T? TakeUnlessStruct<T, TContext>(this T self, in TContext context, Func<TContext, T, bool> predicate)
        where T : struct
    {
        return !predicate(context, self) ? self : null;
    }

    public static TResult Use<T, TResult>(this T disposable, Func<T, TResult> fn) where T : IDisposable
    {
        using (disposable)
        {
            return fn(disposable);
        }
    }

    public static TResult Use<T, TContext, TResult>(
        this T disposable,
        in TContext context,
        Func<TContext, T, TResult> fn
    ) where T : IDisposable
    {
        using (disposable)
        {
            return fn(context, disposable);
        }
    }
}
