namespace Macaron.Functional;

public readonly struct RightEither<TRight>
{
    public readonly TRight Value;

    public RightEither(TRight value)
    {
        Value = value;
    }
}
