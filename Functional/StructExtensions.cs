namespace Macaron.Functional;

public static class StructExtensions
{
    public static T? TakeIf<T>(this T self, Func<T, bool> predicate) where T : struct
    {
        return predicate(self) ? self : null;
    }

    public static T? TakeUnless<T>(this T self, Func<T, bool> predicate) where T : struct
    {
        return !predicate(self) ? self : null;
    }
}
