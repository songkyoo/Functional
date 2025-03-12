namespace Macaron.Functional;

public readonly struct RightEither<TLeft, TRight>
{
    public readonly TRight Value;

    public RightEither(TRight value)
    {
        Value = value;
    }
}
