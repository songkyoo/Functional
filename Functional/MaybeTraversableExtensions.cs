using System.Buffers;

using static Macaron.Functional.Utility;

namespace Macaron.Functional;

public static class MaybeTraversableExtensions
{
    public static Maybe<IEnumerable<TResult>> Traverse<T, TResult>(
        this IEnumerable<T> enumerable,
        Func<T, Maybe<TResult>> fn
    )
    {
        var pool = ArrayPool<TResult>.Shared;
        var buffer = pool.Rent(minimumLength: 8);

        try
        {
            var length = 0;

            foreach (var value in enumerable)
            {
                var maybe = fn(value);

                if (maybe.IsNothing)
                {
                    return Maybe.Nothing<IEnumerable<TResult>>();
                }

                if (length == buffer.Length)
                {
                    var newBuffer = pool.Rent(minimumLength: length * 2);

                    Array.Copy(sourceArray: buffer, destinationArray: newBuffer, length);
                    pool.Return(buffer, clearArray: true);

                    buffer = newBuffer;
                }

                buffer[length] = maybe.Value;
                length += 1;
            }

            var results = new TResult[length];

            Array.Copy(sourceArray: buffer, destinationArray: results, length: length);

            return Maybe.Just<IEnumerable<TResult>>(results);
        }
        finally
        {
            pool.Return(buffer, clearArray: true);
        }
    }

    public static Maybe<IEnumerable<T>> Sequence<T>(this IEnumerable<Maybe<T>> values)
    {
        return values.Traverse(Identity);
    }
}
