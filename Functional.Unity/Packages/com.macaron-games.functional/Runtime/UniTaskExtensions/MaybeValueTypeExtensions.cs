#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System.Threading;
using Cysharp.Threading.Tasks;

namespace Macaron.Functional.UniTaskExtensions
{
    public static class MaybeValueTypeExtensions
    {
        #region GetOrNullAsync
        public static async UniTask<T?> GetOrNullAsync<T>(
            this UniTask<Maybe<T>> maybe,
            CancellationToken cancellationToken = default
        ) where T : struct
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust ? result.Value : null;
        }
        #endregion
    }
}
#endif
