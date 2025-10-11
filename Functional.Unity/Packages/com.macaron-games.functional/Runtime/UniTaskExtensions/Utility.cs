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
            Func<CancellationToken, UniTask> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return fnAsync(cancellationToken);
        }

        public static UniTask<TResult> RunAsync<TResult>(
            Func<CancellationToken, UniTask<TResult>> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return fnAsync(cancellationToken);
        }

        public static async UniTask<Either<Exception, Placeholder>> RunCatchingAsync(
            Func<CancellationToken, UniTask> fnAsync,
            CancellationToken cancellationToken = default
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
            Func<CancellationToken, UniTask<TResult>> fnAsync,
            CancellationToken cancellationToken = default
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
            Func<T, CancellationToken, UniTask> fnAsync,
            T context,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return fnAsync(context, cancellationToken);
        }

        public static UniTask<TResult> RunAsync<T, TResult>(
            Func<T, CancellationToken, UniTask<TResult>> fnAsync,
            T context,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return fnAsync(context, cancellationToken);
        }

        public static async UniTask<Either<Exception, Placeholder>> RunCatchingAsync<T>(
            Func<T, CancellationToken, UniTask> fnAsync,
            T context,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                await fnAsync(context, cancellationToken);

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
            Func<T, CancellationToken, UniTask<TResult>> fnAsync,
            T context,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var result = await fnAsync(context, cancellationToken);

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
    }
}
#endif
