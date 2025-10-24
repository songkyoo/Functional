#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Macaron.Functional.UniTaskExtensions
{
    public static class Utility
    {
        public static UniTask RunAsync(
            CancellationToken cancellationToken,
            Func<CancellationToken, UniTask> fnAsync
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return fnAsync(cancellationToken);
        }

        public static UniTask<TResult> RunAsync<TResult>(
            CancellationToken cancellationToken,
            Func<CancellationToken, UniTask<TResult>> fnAsync
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return fnAsync(cancellationToken);
        }

        public static async UniTask<Either<Exception, Placeholder>> RunCatchingAsync(
            CancellationToken cancellationToken,
            Func<CancellationToken, UniTask> fnAsync
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await fnAsync(cancellationToken);

                return Either.Right<Exception, Placeholder>(Placeholder._);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                return Either.Left<Exception, Placeholder>(exception);
            }
        }

        public static async UniTask<Either<Exception, TResult>> RunCatchingAsync<TResult>(
            CancellationToken cancellationToken,
            Func<CancellationToken, UniTask<TResult>> fnAsync
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var result = await fnAsync(cancellationToken);

                return Either.Right<Exception, TResult>(result);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                return Either.Left<Exception, TResult>(exception);
            }
        }

        public static UniTask RunAsync<T>(
            T value,
            CancellationToken cancellationToken,
            Func<T, CancellationToken, UniTask> fnAsync
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return fnAsync(value, cancellationToken);
        }

        public static UniTask<TResult> RunAsync<T, TResult>(
            T value,
            CancellationToken cancellationToken,
            Func<T, CancellationToken, UniTask<TResult>> fnAsync
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return fnAsync(value, cancellationToken);
        }

        public static async UniTask<Either<Exception, Placeholder>> RunCatchingAsync<T>(
            T value,
            CancellationToken cancellationToken,
            Func<T, CancellationToken, UniTask> fnAsync
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await fnAsync(value, cancellationToken);

                return Either.Right<Exception, Placeholder>(Placeholder._);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                return Either.Left<Exception, Placeholder>(exception);
            }
        }

        public static async UniTask<Either<Exception, TResult>> RunCatchingAsync<T, TResult>(
            T value,
            CancellationToken cancellationToken,
            Func<T, CancellationToken, UniTask<TResult>> fnAsync
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var result = await fnAsync(value, cancellationToken);

                return Either.Right<Exception, TResult>(result);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                return Either.Left<Exception, TResult>(exception);
            }
        }

        public static async UniTask UseAsync<T>(T disposable, Action<T> action)
            where T : IAsyncDisposable
        {
            await using (disposable)
            {
                action(disposable);
            }
        }

        public static async UniTask<TResult> UseAsync<T, TResult>(T disposable, Func<T, TResult> fn)
            where T : IAsyncDisposable
        {
            await using (disposable)
            {
                return fn(disposable);
            }
        }

        public static async UniTask UseAsync<T>(
            T disposable,
            CancellationToken cancellationToken,
            Func<T, CancellationToken, UniTask> fnAsync
        ) where T : IAsyncDisposable
        {
            await using (disposable)
            {
                cancellationToken.ThrowIfCancellationRequested();

                await fnAsync(disposable, cancellationToken);
            }
        }

        public static async UniTask<TResult> UseAsync<T, TResult>(
            T disposable,
            CancellationToken cancellationToken,
            Func<T, CancellationToken, UniTask<TResult>> fnAsync
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
