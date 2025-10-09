namespace Macaron.Functional;

public static partial class EitherExtensions
{
    public static Either<TLeft, TResult> Apply<TLeft, TRight, TResult>(
        this Either<TLeft, Func<TRight, TResult>> fn,
        Either<TLeft, TRight> either
    )
    {
        return fn.FlatMap(either.Map);
    }

    public static Either<TLeft, TRight> OrElse<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Either<TLeft, TRight> other
    )
    {
        return either.IsRight ? either : other;
    }

    public static Either<TLeft, TRight> OrElse<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Func<TLeft, Either<TLeft, TRight>> getOther
    )
    {
        return either.IsRight ? either : getOther(either.Left);
    }

    public static Either<TLeft, TRight> Ensure<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Func<TRight, bool> predicate,
        TLeft left
    )
    {
        if (either.IsRight)
        {
            return predicate(either.Right) ? either : Either.Left<TLeft, TRight>(left);
        }

        return either;
    }

    public static Either<TLeft, TRight> Ensure<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Func<TRight, bool> predicate,
        Func<TRight, TLeft> getLeft
    )
    {
        if (either.IsRight)
        {
            var right = either.Right;

            return predicate(right) ? either : Either.Left<TLeft, TRight>(getLeft(right));
        }

        return either;
    }

    public static Either<TLeft, TRight> Recover<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        TRight right
    )
    {
        return either.IsRight ? either : Either.Right<TLeft, TRight>(right);
    }

    public static Either<TLeft, TRight> Recover<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Func<TLeft, TRight> getRight
    )
    {
        return either.IsRight ? either : Either.Right<TLeft, TRight>(getRight(either.Left));
    }

    public static TRight? GetOrNull<TLeft, TRight>(this Either<TLeft, TRight> either)
        where TRight : class
    {
        return either.IsRight ? either.Right : null;
    }

    public static TLeft? GetLeftOrNull<TLeft, TRight>(this Either<TLeft, TRight> either)
        where TLeft : class
    {
        return either.IsLeft ? either.Left : null;
    }

    public static TRight GetOrElse<TLeft, TRight>(this Either<TLeft, TRight> either, TRight right)
    {
        return either.IsRight ? either.Right : right;
    }

    public static TRight GetOrElse<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, TRight> getRight)
    {
        return either.IsRight ? either.Right : getRight(either.Left);
    }

    public static TLeft GetLeftOrElse<TLeft, TRight>(this Either<TLeft, TRight> either, TLeft left)
    {
        return either.IsLeft ? either.Left : left;
    }

    public static TLeft GetLeftOrElse<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TRight, TLeft> getLeft)
    {
        return either.IsLeft ? either.Left : getLeft(either.Right);
    }

    public static bool TryGet<TLeft, TRight>(this Either<TLeft, TRight> either, out TRight right)
    {
        if (either.IsRight)
        {
            right = either.Right;

            return true;
        }
        else
        {
            right = default!;

            return false;
        }
    }

    public static bool TryGetLeft<TLeft, TRight>(this Either<TLeft, TRight> either, out TLeft left)
    {
        if (either.IsLeft)
        {
            left = either.Left;

            return true;
        }
        else
        {
            left = default!;

            return false;
        }
    }

    public static Maybe<TRight> ToMaybe<TLeft, TRight>(this Either<TLeft, TRight> either)
    {
        return either.IsRight ? Maybe.Just(either.Right) : Maybe.Nothing<TRight>();
    }
}
