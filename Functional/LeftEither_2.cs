namespace Macaron.Functional;

public readonly struct LeftEither<TLeft, TRight>
{
    public readonly TLeft Value;

    public LeftEither(TLeft value)
    {
        Value = value;
    }
}
