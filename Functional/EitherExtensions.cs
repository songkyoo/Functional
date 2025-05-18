namespace Macaron.Functional;

public static partial class EitherExtensions
{
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

    public static T GetOrThrow<TException, T>(this Either<TException, T> either)
        where TException : Exception
    {
        return either.IsRight ? either.Right : throw either.Left;
    }

    public static TRight GetOrElse<TLeft, TRight>(this Either<TLeft, TRight> either, TRight right)
    {
        return either.IsRight ? either.Right : right;
    }

    public static TRight GetOrElse<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TRight> getRight)
    {
        return either.IsRight ? either.Right : getRight();
    }

    public static TRight GetOrElse<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, TRight> getRight)
    {
        return either.IsRight ? either.Right : getRight(either.Left);
    }

    public static Either<TLeft, TRight> OrElse<TLeft, TRight>(this Either<TLeft, TRight> either, TRight right)
    {
        return either.IsRight ? either : Either.Right(right);
    }

    public static Either<TLeft, TRight> OrElse<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TRight> getRight)
    {
        return either.IsRight ? either : Either.Right(getRight());
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
        Func<Either<TLeft, TRight>> getOther
    )
    {
        return either.IsRight ? either : getOther();
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
            return predicate(either.Right) ? either : Either.Left(left);
        }

        return either;
    }

    public static Either<TLeft, TRight> Ensure<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Func<TRight, bool> predicate,
        Func<TLeft> getLeft
    )
    {
        if (either.IsRight)
        {
            return predicate(either.Right) ? either : Either.Left(getLeft());
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
            return predicate(either.Right) ? either : Either.Left(getLeft(either.Right));
        }

        return either;
    }

    public static Either<TLeft, TRight> Ensure<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Func<TRight, bool> predicate,
        Either<TLeft, TRight> other
    )
    {
        return predicate(either.Right) ? either : other;
    }

    public static Either<TLeft, TRight> Ensure<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Func<TRight, bool> predicate,
        Func<Either<TLeft, TRight>> getOther
    )
    {
        if (either.IsRight)
        {
            return predicate(either.Right) ? either : getOther();
        }

        return either;
    }

    public static Either<TLeft, TRight> Ensure<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Func<TRight, bool> predicate,
        Func<TRight, Either<TLeft, TRight>> getOther
    )
    {
        if (either.IsRight)
        {
            return predicate(either.Right) ? either : getOther(either.Right);
        }

        return either;
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

    public static Either<TLeft, TResult> Apply<TLeft, TRight, TResult>(
        this Either<TLeft, Func<TRight, TResult>> fn,
        Either<TLeft, TRight> either
    )
    {
        if (fn.IsLeft)
        {
            return Either.Left<TLeft, TResult>(fn.Left);
        }

        if (either.IsLeft)
        {
            return Either.Left<TLeft, TResult>(either.Left);
        }

        var f = fn.Right;
        var value = either.Right;
        var result = f(value);

        return Either.Right<TLeft, TResult>(result);
    }
}
