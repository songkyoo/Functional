namespace Macaron.Functional;

public readonly struct LeftEither<TLeft>
{
    public readonly TLeft Value;

    public LeftEither(in TLeft value)
    {
        Value = value;
    }
}
