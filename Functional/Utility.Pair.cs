namespace Macaron.Functional;

partial class Utility
{
    public static KeyValuePair<TKey, TValue> Pair<TKey, TValue>(
        TKey key, TValue value
    )
    {
        return new KeyValuePair<TKey, TValue>(key, value);
    }

    public static Action<KeyValuePair<TKey, TValue>> ToPaired<TKey, TValue>(
        Action<TKey, TValue> action
    )
    {
        return pair => action(pair.Key, pair.Value);
    }

    public static Func<KeyValuePair<TKey, TValue>, TResult> ToPaired<TKey, TValue, TResult>(
        Func<TKey, TValue, TResult> fn
    )
    {
        return pair => fn(pair.Key, pair.Value);
    }

    public static Action<TKey, TValue> FromPaired<TKey, TValue>(
        Action<KeyValuePair<TKey, TValue>> action
    )
    {
        return (key, value) => action(Pair(key, value));
    }

    public static Func<TKey, TValue, TResult> FromPaired<TKey, TValue, TResult>(
        Func<KeyValuePair<TKey, TValue>, TResult> fn
    )
    {
        return (key, value) => fn(Pair(key, value));
    }
}
