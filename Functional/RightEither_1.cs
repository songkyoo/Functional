namespace Macaron.Functional;

public struct RightEither<TRight>
{
    public readonly TRight Value;

    public RightEither(TRight value)
    {
        Value = value;
    }
}
