namespace Macaron.Functional;

public static class Maybe
{
    public static Maybe<T> Just<T>(in T value)
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
}
