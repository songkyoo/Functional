namespace Macaron.Functional;

public static partial class MaybeExtensions
{
    public static Maybe<TResult> Select<T, TResult>(this Maybe<T> maybe, Func<T, TResult> selector)
    {
        return maybe.Map(selector);
    }

    public static Maybe<TResult> SelectMany<T, TResult>(this Maybe<T> maybe, Func<T, Maybe<TResult>> selector)
    {
        return maybe.FlatMap(selector);
    }

    public static Maybe<T> Where<T>(this Maybe<T> maybe, Func<T, bool> predicate)
    {
        return maybe is { IsJust: true, Value: var value } && predicate(value) ? maybe : Maybe.Nothing<T>();
    }

    public static T GetOrElse<T>(this Maybe<T> maybe, Func<T> getDefaultValue)
    {
        return maybe.IsJust ? maybe.Value : getDefaultValue();
    }

    public static Maybe<T> OrElse<T>(this Maybe<T> maybe, Func<T> getDefaultValue)
    {
        return maybe.IsJust ? maybe : Maybe.Just(getDefaultValue());
    }

    public static Maybe<T> OrElse<T>(this Maybe<T> maybe, Maybe<T> other)
    {
        return maybe.IsJust ? maybe : other;
    }

    public static Maybe<T> OrElse<T>(this Maybe<T> maybe, Func<Maybe<T>> getDefaultValue)
    {
        return maybe.IsJust ? maybe : getDefaultValue();
    }

    public static Maybe<TResult> Apply<T, TResult>(this Maybe<Func<T, TResult>> fn, Maybe<T> maybe)
    {
        return fn.FlatMap(maybe.Map);
    }
}
