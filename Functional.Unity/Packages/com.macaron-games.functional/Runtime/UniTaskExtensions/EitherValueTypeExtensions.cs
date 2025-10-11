#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System.Threading;
using Cysharp.Threading.Tasks;

namespace Macaron.Functional.UniTaskExtensions
{
    public static class EitherValueTypeExtensions
    {
        #region GetOrNullAsync
        public static async UniTask<TRight?> GetOrNullAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            CancellationToken cancellationToken = default
        ) where TRight : struct
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight ? result.Right : null;
        }
        #endregion

        #region GetLeftOrNullAsync
        public static async UniTask<TLeft?> GetLeftOrNullAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            CancellationToken cancellationToken = default
        ) where TLeft : struct
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsLeft ? result.Left : null;
        }
        #endregion
    }
}
#endif
