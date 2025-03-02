namespace Macaron.Functional;

public static class EitherExtensions
{
    public static TRight GetOrElse<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TRight> supplier)
    {
        return either.IsRight ? either.Right : supplier.Invoke();
    }

    public static Either<TLeft, TRight> OrElse<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Func<TRight> supplier
    )
    {
        return either.IsRight ? either : Either.Right(supplier.Invoke());
    }

    public static Either<TLeft, TRight> OrElse<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        in Either<TLeft, TRight> other
    )
    {
        return either.IsRight ? either : other;
    }

    public static Either<TLeft, TRight> OrElse<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Func<Either<TLeft, TRight>> supplier
    )
    {
        return either.IsRight ? either : supplier.Invoke();
    }

    public static Either<TLeft, TResult> Apply<TLeft, TRight, TResult>(
        this Either<TLeft, Func<TRight, TResult>> fn, Either<TLeft, TRight> either
    )
    {
        if (fn.IsRight)
        {
            return either.Map(fn.Right);
        }
        else
        {
            return Either.Left<TLeft, TResult>(fn.Left);
        }
    }

    public static Func<Either<L, T>, Either<L, TResult>> Lift<L, T, TResult>(Func<T, TResult> fn)
    {
        return either => either.Map(fn);
    }
}
