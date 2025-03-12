namespace Macaron.Functional;

partial class Utility
{
    public static KeyValuePair<TKey, TValue> Pair<TKey, TValue>(TKey key, TValue value)
    {
        return new KeyValuePair<TKey, TValue>(key, value);
    }

    public static Action<KeyValuePair<TKey, TValue>> ToPaired<TKey, TValue>(Action<TKey, TValue> action)
    {
        return pair => action(pair.Key, pair.Value);
    }

    public static Func<KeyValuePair<TKey, TValue>, TResult> FromPaired<TKey, TValue, TResult>(
        Func<TKey, TValue, TResult> fn
    )
    {
        return pair => fn(pair.Key, pair.Value);
    }
}
