namespace Macaron.Functional;

public static partial class MaybeExtensions
{
    public static T? GetOrNull<T>(this Maybe<T> maybe)
        where T : class
    {
        return maybe.IsJust ? maybe.Value : null;
    }

    public static T GetOrElse<T>(this Maybe<T> maybe, T value)
    {
        return maybe.IsJust ? maybe.Value : value;
    }

    public static T GetOrElse<T>(this Maybe<T> maybe, Func<T> getValue)
    {
        return maybe.IsJust ? maybe.Value : getValue();
    }

    public static Maybe<T> OrElse<T>(this Maybe<T> maybe, T value)
    {
        return maybe.IsJust ? maybe : Maybe.Just(value);
    }

    public static Maybe<T> OrElse<T>(this Maybe<T> maybe, Func<T> getValue)
    {
        return maybe.IsJust ? maybe : Maybe.Just(getValue());
    }

    public static Maybe<T> OrElse<T>(this Maybe<T> maybe, Maybe<T> other)
    {
        return maybe.IsJust ? maybe : other;
    }

    public static Maybe<T> OrElse<T>(this Maybe<T> maybe, Func<Maybe<T>> getOther)
    {
        return maybe.IsJust ? maybe : getOther();
    }

    public static Maybe<T> Ensure<T>(this Maybe<T> maybe, Func<T, bool> predicate)
    {
        if (maybe.IsJust)
        {
            return predicate(maybe.Value) ? maybe : Maybe.Nothing<T>();
        }

        return maybe;
    }

    public static bool TryGet<T>(this Maybe<T> maybe, out T value)
    {
        if (maybe.IsJust)
        {
            value = maybe.Value;
            return true;
        }
        else
        {
            value = default!;
            return false;
        }
    }

    public static Maybe<TResult> Apply<T, TResult>(this Maybe<Func<T, TResult>> fn, Maybe<T> maybe)
    {
        return fn.FlatMap(maybe.Map);
    }

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
        if (maybe.IsJust)
        {
            return predicate(maybe.Value) ? maybe : Maybe.Nothing<T>();
        }

        return maybe;
    }
}
