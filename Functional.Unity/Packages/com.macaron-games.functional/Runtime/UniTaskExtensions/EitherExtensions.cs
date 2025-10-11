#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Macaron.Functional.UniTaskExtensions
{
    public static class EitherExtensions
    {
        #region MapAsync
        public static async UniTask<Either<TLeft, TResult>> MapAsync<TLeft, TRight, TResult>(
            this Either<TLeft, TRight> either,
            Func<TRight, CancellationToken, UniTask<TResult>> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return either.IsRight
                ? Either.Right<TLeft, TResult>(await fnAsync(either.Right, cancellationToken))
                : Either.Left<TLeft, TResult>(either.Left);
        }

        public static async UniTask<Either<TLeft, TResult>> MapAsync<TLeft, TRight, TResult>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TRight, CancellationToken, UniTask<TResult>> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight
                ? Either.Right<TLeft, TResult>(await fnAsync(result.Right, cancellationToken))
                : Either.Left<TLeft, TResult>(result.Left);
        }

        public static async UniTask<Either<TLeft, TResult>> MapAsync<TLeft, TRight, TResult>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TRight, TResult> fn,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight
                ? Either.Right<TLeft, TResult>(fn(result.Right))
                : Either.Left<TLeft, TResult>(result.Left);
        }
        #endregion

        #region MapLeftAsync
        public static async UniTask<Either<TResult, TRight>> MapLeftAsync<TLeft, TRight, TResult>(
            this Either<TLeft, TRight> either,
            Func<TLeft, CancellationToken, UniTask<TResult>> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return either.IsLeft
                ? Either.Left<TResult, TRight>(await fnAsync(either.Left, cancellationToken))
                : Either.Right<TResult, TRight>(either.Right);
        }

        public static async UniTask<Either<TResult, TRight>> MapLeftAsync<TLeft, TRight, TResult>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TLeft, CancellationToken, UniTask<TResult>> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsLeft
                ? Either.Left<TResult, TRight>(await fnAsync(result.Left, cancellationToken))
                : Either.Right<TResult, TRight>(result.Right);
        }

        public static async UniTask<Either<TResult, TRight>> MapLeftAsync<TLeft, TRight, TResult>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TLeft, TResult> fn,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsLeft
                ? Either.Left<TResult, TRight>(fn(result.Left))
                : Either.Right<TResult, TRight>(result.Right);
        }
        #endregion

        #region FlatMapAsync
        public static async UniTask<Either<TLeft, TResult>> FlatMapAsync<TLeft, TRight, TResult>(
            this Either<TLeft, TRight> either,
            Func<TRight, CancellationToken, UniTask<Either<TLeft, TResult>>> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return either.IsRight
                ? await fnAsync(either.Right, cancellationToken)
                : Either.Left<TLeft, TResult>(either.Left);
        }

        public static async UniTask<Either<TLeft, TResult>> FlatMapAsync<TLeft, TRight, TResult>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TRight, CancellationToken, UniTask<Either<TLeft, TResult>>> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight
                ? await fnAsync(result.Right, cancellationToken)
                : Either.Left<TLeft, TResult>(result.Left);
        }

        public static async UniTask<Either<TLeft, TResult>> FlatMapAsync<TLeft, TRight, TResult>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TRight, Either<TLeft, TResult>> fn,
            CancellationToken cancellationToken = default
        )
        {
            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight
                ? fn(result.Right)
                : Either.Left<TLeft, TResult>(result.Left);
        }
        #endregion

        #region FlatMapLeftAsync
        public static async UniTask<Either<TResult, TRight>> FlatMapLeftAsync<TLeft, TRight, TResult>(
            this Either<TLeft, TRight> either,
            Func<TLeft, CancellationToken, UniTask<Either<TResult, TRight>>> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return either.IsLeft
                ? await fnAsync(either.Left, cancellationToken)
                : Either.Right<TResult, TRight>(either.Right);
        }

        public static async UniTask<Either<TResult, TRight>> FlatMapLeftAsync<TLeft, TRight, TResult>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TLeft, CancellationToken, UniTask<Either<TResult, TRight>>> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsLeft
                ? await fnAsync(result.Left, cancellationToken)
                : Either.Right<TResult, TRight>(result.Right);
        }

        public static async UniTask<Either<TResult, TRight>> FlatMapLeftAsync<TLeft, TRight, TResult>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TLeft, Either<TResult, TRight>> fn,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsLeft
                ? fn(result.Left)
                : Either.Right<TResult, TRight>(result.Right);
        }
        #endregion

        #region TapAsync
        public static async UniTask<Either<TLeft, TRight>> TapAsync<TLeft, TRight>(
            this Either<TLeft, TRight> either,
            Func<TRight, CancellationToken, UniTask> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (either.IsRight)
            {
                await fnAsync(either.Right, cancellationToken);
            }

            return either;
        }

        public static async UniTask<Either<TLeft, TRight>> TapAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TRight, CancellationToken, UniTask> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            if (result.IsRight)
            {
                await fnAsync(result.Right, cancellationToken);
            }

            return result;
        }

        public static async UniTask<Either<TLeft, TRight>> TapAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Action<TRight> action,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            if (result.IsRight)
            {
                action(result.Right);
            }

            return result;
        }
        #endregion

        #region TapLeftAsync
        public static async UniTask<Either<TLeft, TRight>> TapLeftAsync<TLeft, TRight>(
            this Either<TLeft, TRight> either,
            Func<TLeft, CancellationToken, UniTask> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (either.IsLeft)
            {
                await fnAsync(either.Left, cancellationToken);
            }

            return either;
        }

        public static async UniTask<Either<TLeft, TRight>> TapLeftAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TLeft, CancellationToken, UniTask> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            if (result.IsLeft)
            {
                await fnAsync(result.Left, cancellationToken);
            }

            return result;
        }

        public static async UniTask<Either<TLeft, TRight>> TapLeftAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Action<TLeft> action,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            if (result.IsLeft)
            {
                action(result.Left);
            }

            return result;
        }
        #endregion

        #region MatchAsync
        public static UniTask MatchAsync<TLeft, TRight>(
            this Either<TLeft, TRight> either,
            Func<TLeft, CancellationToken, UniTask> leftAsync,
            Func<TRight, CancellationToken, UniTask> rightAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return either.IsRight
                ? rightAsync(either.Right, cancellationToken)
                : leftAsync(either.Left, cancellationToken);
        }

        public static UniTask MatchAsync<TLeft, TRight, TResult>(
            this Either<TLeft, TRight> either,
            Func<TLeft, CancellationToken, UniTask<TResult>> leftAsync,
            Func<TRight, CancellationToken, UniTask<TResult>> rightAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return either.IsRight
                ? rightAsync(either.Right, cancellationToken)
                : leftAsync(either.Left, cancellationToken);
        }

        public static async UniTask MatchAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TLeft, CancellationToken, UniTask> leftAsync,
            Func<TRight, CancellationToken, UniTask> rightAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            await (result.IsRight
                ? rightAsync(result.Right, cancellationToken)
                : leftAsync(result.Left, cancellationToken));
        }

        public static async UniTask<TResult> MatchAsync<TLeft, TRight, TResult>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TLeft, CancellationToken, UniTask<TResult>> leftAsync,
            Func<TRight, CancellationToken, UniTask<TResult>> rightAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return await (result.IsRight
                ? rightAsync(result.Right, cancellationToken)
                : leftAsync(result.Left, cancellationToken));
        }

        public static async UniTask MatchAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Action<TLeft> left,
            Action<TRight> right,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            if (result.IsRight)
            {
                right(result.Right);
            }
            else
            {
                left(result.Left);
            }
        }

        public static async UniTask<TResult> MatchAsync<TLeft, TRight, TResult>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TLeft, TResult> left,
            Func<TRight, TResult> right,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight ? right(result.Right) : left(result.Left);
        }
        #endregion

        #region OrElseAsync
        public static async UniTask<Either<TLeft, TRight>> OrElseAsync<TLeft, TRight>(
            this Either<TLeft, TRight> either,
            Func<CancellationToken, UniTask<Either<TLeft, TRight>>> getOtherAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return either.IsRight ? either : await getOtherAsync(cancellationToken);
        }

        public static async UniTask<Either<TLeft, TRight>> OrElseAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<CancellationToken, UniTask<Either<TLeft, TRight>>> getOtherAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight ? result : await getOtherAsync(cancellationToken);
        }

        public static async UniTask<Either<TLeft, TRight>> OrElseAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Either<TLeft, TRight> other,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight ? result : other;
        }

        public static async UniTask<Either<TLeft, TRight>> OrElseAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<Either<TLeft, TRight>> getOther,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight ? result : getOther();
        }
        #endregion

        #region EnsureAsync
        public static async UniTask<Either<TLeft, TRight>> EnsureAsync<TLeft, TRight>(
            this Either<TLeft, TRight> either,
            Func<TRight, CancellationToken, UniTask<bool>> predicateAsync,
            TLeft left,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (either.IsRight)
            {
                return await predicateAsync(either.Right, cancellationToken)
                    ? either
                    : Either.Left<TLeft, TRight>(left);
            }

            return either;
        }

        public static async UniTask<Either<TLeft, TRight>> EnsureAsync<TLeft, TRight>(
            this Either<TLeft, TRight> either,
            Func<TRight, CancellationToken, UniTask<bool>> predicateAsync,
            Func<TRight, TLeft> getLeft,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (either.IsRight)
            {
                var right = either.Right;

                return await predicateAsync(right, cancellationToken)
                    ? either
                    : Either.Left<TLeft, TRight>(getLeft(right));
            }

            return either;
        }

        public static async UniTask<Either<TLeft, TRight>> EnsureAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TRight, CancellationToken, UniTask<bool>> predicateAsync,
            TLeft left,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            if (result.IsRight)
            {
                return await predicateAsync(result.Right, cancellationToken)
                    ? result
                    : Either.Left<TLeft, TRight>(left);
            }

            return result;
        }

        public static async UniTask<Either<TLeft, TRight>> EnsureAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TRight, CancellationToken, UniTask<bool>> predicateAsync,
            Func<TRight, TLeft> getLeft,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            if (result.IsRight)
            {
                var right = result.Right;

                return await predicateAsync(right, cancellationToken)
                    ? result
                    : Either.Left<TLeft, TRight>(getLeft(right));
            }

            return result;
        }

        public static async UniTask<Either<TLeft, TRight>> EnsureAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TRight, bool> predicate,
            TLeft left,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            if (result.IsRight)
            {
                return predicate(result.Right) ? result : Either.Left<TLeft, TRight>(left);
            }

            return result;
        }

        public static async UniTask<Either<TLeft, TRight>> EnsureAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TRight, bool> predicate,
            Func<TRight, TLeft> getLeft,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            if (result.IsRight)
            {
                var right = result.Right;

                return predicate(right) ? result : Either.Left<TLeft, TRight>(getLeft(right));
            }

            return result;
        }
        #endregion

        #region RecoverAsync
        public static async UniTask<Either<TLeft, TRight>> RecoverAsync<TLeft, TRight>(
            this Either<TLeft, TRight> either,
            Func<CancellationToken, UniTask<TRight>> getRightAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return either.IsRight ? either : Either.Right<TLeft, TRight>(await getRightAsync(cancellationToken));
        }

        public static async UniTask<Either<TLeft, TRight>> RecoverAsync<TLeft, TRight>(
            this Either<TLeft, TRight> either,
            Func<TLeft, CancellationToken, UniTask<TRight>> getRightAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return either.IsRight
                ? either
                : Either.Right<TLeft, TRight>(await getRightAsync(either.Left, cancellationToken));
        }

        public static async UniTask<Either<TLeft, TRight>> RecoverAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<CancellationToken, UniTask<TRight>> getRightAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight ? result : Either.Right<TLeft, TRight>(await getRightAsync(cancellationToken));
        }

        public static async UniTask<Either<TLeft, TRight>> RecoverAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TLeft, CancellationToken, UniTask<TRight>> getRightAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight
                ? result
                : Either.Right<TLeft, TRight>(await getRightAsync(result.Left, cancellationToken));
        }

        public static async UniTask<Either<TLeft, TRight>> RecoverAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            TRight right,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight ? result : Either.Right<TLeft, TRight>(right);
        }

        public static async UniTask<Either<TLeft, TRight>> RecoverAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TLeft, TRight> getRight,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight ? result : Either.Right<TLeft, TRight>(getRight(result.Left));
        }
        #endregion

        #region GetOrNullAsync
        public static async UniTask<TRight?> GetOrNullAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            CancellationToken cancellationToken = default
        ) where TRight : class
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
        ) where TLeft : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsLeft ? result.Left : null;
        }
        #endregion

        #region GetOrElseAsync
        public static UniTask<TRight> GetOrElseAsync<TLeft, TRight>(
            this Either<TLeft, TRight> either,
            Func<TLeft, CancellationToken, UniTask<TRight>> getRightAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return either.IsRight ? UniTask.FromResult(either.Right) : getRightAsync(either.Left, cancellationToken);
        }

        public static async UniTask<TRight> GetOrElseAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TLeft, CancellationToken, UniTask<TRight>> getRightAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight ? result.Right : await getRightAsync(result.Left, cancellationToken);
        }

        public static async UniTask<TRight> GetOrElseAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            TRight right,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight ? result.Right : right;
        }

        public static async UniTask<TRight> GetOrElseAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TLeft, TRight> getRight,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsRight ? result.Right : getRight(result.Left);
        }
        #endregion

        #region GetLeftOrElseAsync
        public static UniTask<TLeft> GetLeftOrElseAsync<TLeft, TRight>(
            this Either<TLeft, TRight> either,
            Func<TRight, CancellationToken, UniTask<TLeft>> getLeftAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return either.IsLeft ? UniTask.FromResult(either.Left) : getLeftAsync(either.Right, cancellationToken);
        }

        public static async UniTask<TLeft> GetLeftOrElseAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TRight, CancellationToken, UniTask<TLeft>> getLeftAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsLeft ? result.Left : await getLeftAsync(result.Right, cancellationToken);
        }

        public static async UniTask<TLeft> GetLeftOrElseAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            TLeft left,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsLeft ? result.Left : left;
        }

        public static async UniTask<TLeft> GetLeftOrElseAsync<TLeft, TRight>(
            this UniTask<Either<TLeft, TRight>> either,
            Func<TRight, TLeft> getLeft,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await either.AttachExternalCancellation(cancellationToken);

            return result.IsLeft ? result.Left : getLeft(result.Right);
        }
        #endregion
    }
}
#endif
