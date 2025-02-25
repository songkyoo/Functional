namespace Macaron.Functional;

public readonly struct JustMaybe<T>
{
    public readonly T Value;

    public JustMaybe(in T value)
    {
        Value = value;
    }
}
