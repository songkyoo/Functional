#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Macaron.Functional.UniTaskExtensions
{
    public static partial class Extensions
    {
        public static async UniTask UseAsync<T>(this T disposable, Action<T> action)
            where T : IAsyncDisposable
        {
            await using (disposable)
            {
                action(disposable);
            }
        }

        public static async UniTask<TResult> UseAsync<T, TResult>(this T disposable, Func<T, TResult> fn)
            where T : IAsyncDisposable
        {
            await using (disposable)
            {
                return fn(disposable);
            }
        }

        public static async UniTask UseAsync<T>(
            this T disposable,
            Func<T, CancellationToken, UniTask> fnAsync,
            CancellationToken cancellationToken = default
        ) where T : IAsyncDisposable
        {
            await using (disposable)
            {
                cancellationToken.ThrowIfCancellationRequested();

                await fnAsync(disposable, cancellationToken);
            }
        }

        public static async UniTask<TResult> UseAsync<T, TResult>(
            this T disposable,
            Func<T, CancellationToken, UniTask<TResult>> fnAsync,
            CancellationToken cancellationToken = default
        ) where T : IAsyncDisposable
        {
            await using (disposable)
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await fnAsync(disposable, cancellationToken);
            }
        }
    }
}
#endif
