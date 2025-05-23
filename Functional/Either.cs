namespace Macaron.Functional;

public static partial class Either
{
    public static LeftEither<TLeft> Left<TLeft>(TLeft value)
    {
        return new LeftEither<TLeft>(value);
    }

    public static Either<TLeft, TRight> Left<TLeft, TRight>(TLeft value)
    {
        return new LeftEither<TLeft, TRight>(value);
    }

    public static RightEither<TRight> Right<TRight>(TRight value)
    {
        return new RightEither<TRight>(value);
    }

    public static Either<TLeft, TRight> Right<TLeft, TRight>(TRight value)
    {
        return new RightEither<TLeft, TRight>(value);
    }

    public static Func<Either<TLeft, TRight>, Either<TLeft, TResult>> Lift<TLeft, TRight, TResult>(
        Func<TRight, TResult> fn
    )
    {
        return either => either.Map(fn);
    }
}
