namespace Macaron.Functional;

public struct RightEither<TRight>
{
    public readonly TRight Value;

    public RightEither(in TRight value)
    {
        Value = value;
    }
}
