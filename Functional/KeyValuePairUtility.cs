namespace Macaron.Functional;

public static class KeyValuePairUtility
{
    public static KeyValuePair<TKey, TValue> Pair<TKey, TValue>(in TKey key, in TValue value)
    {
        return new KeyValuePair<TKey, TValue>(key, value);
    }

    public static Action<KeyValuePair<TKey, TValue>> Apply<TKey, TValue>(Action<TKey, TValue> action)
    {
        return pair => action(pair.Key, pair.Value);
    }

    public static Func<KeyValuePair<TKey, TValue>, TResult> Apply<TKey, TValue, TResult>(
        Func<TKey, TValue, TResult> fn
    )
    {
        return pair => fn(pair.Key, pair.Value);
    }
}
