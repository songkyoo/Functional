namespace Macaron.Functional;

public static partial class MaybeExtensions
{
    public static T GetOrElse<T>(this Maybe<T> maybe, Func<T> supplier)
    {
        return maybe.IsJust ? maybe.Value : supplier.Invoke();
    }

    public static Maybe<T> OrElse<T>(this Maybe<T> maybe, Func<T> supplier)
    {
        return maybe.IsJust ? maybe : Maybe.Just(supplier.Invoke());
    }

    public static Maybe<T> OrElse<T>(this Maybe<T> maybe, in Maybe<T> other)
    {
        return maybe.IsJust ? maybe : other;
    }

    public static Maybe<T> OrElse<T>(this Maybe<T> maybe, Func<Maybe<T>> supplier)
    {
        return maybe.IsJust ? maybe : supplier.Invoke();
    }

    public static Maybe<TResult> Apply<T, TResult>(this Maybe<Func<T, TResult>> fn, Maybe<T> maybe)
    {
        return fn.FlatMap(maybe.Map);
    }

    public static Func<Maybe<T>, Maybe<TResult>> Lift<T, TResult>(Func<T, TResult> fn)
    {
        return maybe => maybe.Map(fn);
    }
}
