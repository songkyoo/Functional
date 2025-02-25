namespace Macaron.Functional;

public static class Either
{
    public static LeftEither<TLeft> Left<TLeft>(in TLeft value)
    {
        return new LeftEither<TLeft>(value);
    }

    public static Either<TLeft, TRight> Left<TLeft, TRight>(in TLeft value)
    {
        return new LeftEither<TLeft, TRight>(value);
    }

    public static RightEither<TRight> Right<TRight>(in TRight value)
    {
        return new RightEither<TRight>(value);
    }

    public static Either<TLeft, TRight> Right<TLeft, TRight>(in TRight value)
    {
        return new RightEither<TLeft, TRight>(value);
    }
}
