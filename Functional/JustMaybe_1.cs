namespace Macaron.Functional;

public readonly struct JustMaybe<T>(T? value)
{
    public readonly T? Value = value;
}
