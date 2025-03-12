namespace Macaron.Functional;

public static partial class MaybeExtensions
{
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
