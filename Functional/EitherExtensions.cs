namespace Macaron.Functional;

public static partial class EitherExtensions
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
        Either<TLeft, TRight> other
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
