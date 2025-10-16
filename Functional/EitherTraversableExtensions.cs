using System.Buffers;

using static Macaron.Functional.Utility;

namespace Macaron.Functional;

public static class EitherTraversableExtensions
{
    public static Either<TLeft, IEnumerable<TResult>> Traverse<TLeft, T, TResult>(
        this IEnumerable<T> enumerable,
        Func<T, Either<TLeft, TResult>> fn
    )
    {
        var pool = ArrayPool<TResult>.Shared;
        var buffer = pool.Rent(minimumLength: 8);

        try
        {
            var length = 0;

            foreach (var value in enumerable)
            {
                var either = fn(value);

                if (either.IsLeft)
                {
                    return Either.Left<TLeft, IEnumerable<TResult>>(either.Left);
                }

                if (length == buffer.Length)
                {
                    var newBuffer = pool.Rent(minimumLength: length * 2);

                    Array.Copy(sourceArray: buffer, destinationArray: newBuffer, length);
                    pool.Return(buffer, clearArray: true);

                    buffer = newBuffer;
                }

                buffer[length] = either.Right;
                length += 1;
            }

            var results = new TResult[length];

            Array.Copy(sourceArray: buffer, destinationArray: results, length: length);

            return Either.Right<TLeft, IEnumerable<TResult>>(results);
        }
        finally
        {
            pool.Return(buffer, clearArray: true);
        }
    }

    public static Either<TLeft, IEnumerable<TRight>> Sequence<TLeft, TRight>(
        this IEnumerable<Either<TLeft, TRight>> values
    )
    {
        return values.Traverse(Identity);
    }
}
