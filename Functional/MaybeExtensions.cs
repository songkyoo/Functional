namespace Macaron.Functional;

public static partial class MaybeExtensions
{
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

    public static Maybe<T> Recover<T>(this Maybe<T> maybe, T value)
    {
        return maybe.IsJust ? maybe : Maybe.Just(value);
    }

    public static Maybe<T> Recover<T>(this Maybe<T> maybe, Func<T> getValue)
    {
        return maybe.IsJust ? maybe : Maybe.Just(getValue());
    }

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

    public static Either<TLeft, TRight> ToEither<TLeft, TRight>(this Maybe<TRight> maybe, TLeft left)
    {
        return maybe.IsJust ? Either.Right<TLeft, TRight>(maybe.Value) : Either.Left<TLeft, TRight>(left);
    }

    public static Either<TLeft, TRight> ToEither<TLeft, TRight>(this Maybe<TRight> maybe, Func<TLeft> getLeft)
    {
        return maybe.IsJust ? Either.Right<TLeft, TRight>(maybe.Value) : Either.Left<TLeft, TRight>(getLeft());
    }
}
