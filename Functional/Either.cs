using System.Runtime.CompilerServices;

namespace Macaron.Functional;

public static partial class Either
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LeftEither<TLeft> Left<TLeft>(TLeft value)
    {
        return new LeftEither<TLeft>(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<TLeft, TRight> Left<TLeft, TRight>(TLeft value)
    {
        return new LeftEither<TLeft, TRight>(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RightEither<TRight> Right<TRight>(TRight value)
    {
        return new RightEither<TRight>(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<TLeft, TRight> Right<TLeft, TRight>(TRight value)
    {
        return new RightEither<TLeft, TRight>(value);
    }
}
