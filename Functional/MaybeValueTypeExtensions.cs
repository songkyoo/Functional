namespace Macaron.Functional;

public static class MaybeValueTypeExtensions
{
    public static T? GetOrNull<T>(this Maybe<T> maybe)
        where T : struct
    {
        return maybe.IsJust ? maybe.Value : null;
    }
}
