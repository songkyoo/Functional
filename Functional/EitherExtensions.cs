namespace Macaron.Functional;

public static class EitherExtensions
{
    public static TRight GetOrElse<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TRight> getDefaultValue)
    {
        return either.IsRight ? either.Right : getDefaultValue();
    }

    public static Either<TLeft, TRight> OrElse<TLeft, TRight>(
        this Either<TLeft, TRight> either,
        Func<TRight> getDefaultValue
    )
    {
        return either.IsRight ? either : Either.Right(getDefaultValue());
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
        Func<Either<TLeft, TRight>> getDefaultValue
    )
    {
        return either.IsRight ? either : getDefaultValue();
    }

    public static Either<TLeft, TResult> Apply<TLeft, TRight, TResult>(
        this Either<TLeft, Func<TRight, TResult>> fn,
        Either<TLeft, TRight> either
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
}
