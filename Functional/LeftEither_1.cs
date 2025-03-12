namespace Macaron.Functional;

public readonly struct LeftEither<TLeft>
{
    public readonly TLeft Value;

    public LeftEither(TLeft value)
    {
        Value = value;
    }
}
