namespace Macaron.Functional;

public static class ValueTypeEitherExtensions
{
    public static TRight? GetOrNull<TLeft, TRight>(this Either<TLeft, TRight> either)
        where TRight : struct
    {
        return either.IsRight ? either.Right : null;
    }

    public static TLeft? GetLeftOrNull<TLeft, TRight>(this Either<TLeft, TRight> either)
        where TLeft : struct
    {
        return either.IsLeft ? either.Left : null;
    }
}
