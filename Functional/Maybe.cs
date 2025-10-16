namespace Macaron.Functional;

public static partial class Maybe
{
    public static Maybe<T> Just<T>(T value)
    {
        return new JustMaybe<T>(value);
    }

    public static NothingMaybe Nothing()
    {
        return new NothingMaybe();
    }

    public static Maybe<T> Nothing<T>()
    {
        return new NothingMaybe<T>();
    }

    public static Maybe<T> Of<T>(T? value) where T : class
    {
        return value != null ? Just(value) : Nothing<T>();
    }

    public static Maybe<T> Of<T>(T? value) where T : struct
    {
        return value != null ? Just(value.Value) : Nothing<T>();
    }
}
