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

    public static Func<Maybe<T>, Maybe<TResult>> Lift<T, TResult>(Func<T, TResult> fn)
    {
        return maybe => maybe.Map(fn);
    }
}
