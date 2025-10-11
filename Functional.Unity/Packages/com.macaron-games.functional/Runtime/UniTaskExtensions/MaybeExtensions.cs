#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Macaron.Functional.UniTaskExtensions
{
    public static class MaybeExtensions
    {
        #region MapAsync
        public static async UniTask<Maybe<TResult>> MapAsync<T, TResult>(
            this Maybe<T> maybe,
            Func<T, CancellationToken, UniTask<TResult>> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return maybe.IsJust switch
            {
                true => Maybe.Just(await fnAsync(maybe.Value, cancellationToken)),
                false => Maybe.Nothing<TResult>(),
            };
        }

        public static async UniTask<Maybe<TResult>> MapAsync<T, TResult>(
            this UniTask<Maybe<T>> maybe,
            Func<T, CancellationToken, UniTask<TResult>> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust switch
            {
                true => Maybe.Just(await fnAsync(result.Value, cancellationToken)),
                false => Maybe.Nothing<TResult>(),
            };
        }

        public static async UniTask<Maybe<TResult>> MapAsync<T, TResult>(
            this UniTask<Maybe<T>> maybe,
            Func<T, TResult> fn,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust switch
            {
                true => Maybe.Just(fn(result.Value)),
                false => Maybe.Nothing<TResult>(),
            };
        }
        #endregion

        #region FlatMapAsync
        public static async UniTask<Maybe<TResult>> FlatMapAsync<T, TResult>(
            this Maybe<T> maybe,
            Func<T, CancellationToken, UniTask<Maybe<TResult>>> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return maybe.IsJust switch
            {
                true => await fnAsync(maybe.Value, cancellationToken),
                false => Maybe.Nothing<TResult>(),
            };
        }

        public static async UniTask<Maybe<TResult>> FlatMapAsync<T, TResult>(
            this UniTask<Maybe<T>> maybe,
            Func<T, CancellationToken, UniTask<Maybe<TResult>>> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust switch
            {
                true => await fnAsync(result.Value, cancellationToken),
                false => Maybe.Nothing<TResult>(),
            };
        }

        public static async UniTask<Maybe<TResult>> FlatMapAsync<T, TResult>(
            this UniTask<Maybe<T>> maybe,
            Func<T, Maybe<TResult>> fn,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust switch
            {
                true => fn(result.Value),
                false => Maybe.Nothing<TResult>(),
            };
        }
        #endregion

        #region TapAsync
        public static async UniTask<Maybe<T>> TapAsync<T>(
            this Maybe<T> maybe,
            Func<T, CancellationToken, UniTask> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (maybe.IsJust)
            {
                await fnAsync(maybe.Value, cancellationToken);
            }

            return maybe;
        }

        public static async UniTask<Maybe<T>> TapAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Func<T, CancellationToken, UniTask> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe;

            if (result.IsJust)
            {
                await fnAsync(result.Value, cancellationToken);
            }

            return result;
        }

        public static async UniTask<Maybe<T>> TapAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Action<T> action,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            if (result.IsJust)
            {
                action(result.Value);
            }

            return result;
        }
        #endregion

        #region TapAsync
        public static async UniTask<Maybe<T>> TapNothingAsync<T>(
            this Maybe<T> maybe,
            Func<CancellationToken, UniTask> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (maybe.IsNothing)
            {
                await fnAsync(cancellationToken);
            }

            return maybe;
        }

        public static async UniTask<Maybe<T>> TapNothingAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Func<CancellationToken, UniTask> fnAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            if (result.IsNothing)
            {
                await fnAsync(cancellationToken);
            }

            return result;
        }

        public static async UniTask<Maybe<T>> TapNothingAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Action action,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            if (result.IsNothing)
            {
                action();
            }

            return result;
        }
        #endregion

        #region MatchAsync
        public static UniTask MatchAsync<T>(
            this Maybe<T> maybe,
            Func<T, CancellationToken, UniTask> justAsync,
            Func<CancellationToken, UniTask> nothingAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return maybe.IsJust ? justAsync(maybe.Value, cancellationToken) : nothingAsync(cancellationToken);
        }

        public static UniTask<TResult> MatchAsync<T, TResult>(
            this Maybe<T> maybe,
            Func<T, CancellationToken, UniTask<TResult>> justAsync,
            Func<CancellationToken, UniTask<TResult>> nothingAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return maybe.IsJust ? justAsync(maybe.Value, cancellationToken) : nothingAsync(cancellationToken);
        }

        public static async UniTask MatchAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Func<T, CancellationToken, UniTask> justAsync,
            Func<CancellationToken, UniTask> nothingAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            await (result.IsJust ? justAsync(result.Value, cancellationToken) : nothingAsync(cancellationToken));
        }

        public static async UniTask<TResult> MatchAsync<T, TResult>(
            this UniTask<Maybe<T>> maybe,
            Func<T, CancellationToken, UniTask<TResult>> justAsync,
            Func<CancellationToken, UniTask<TResult>> nothingAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return await (result.IsJust ? justAsync(result.Value, cancellationToken) : nothingAsync(cancellationToken));
        }

        public static async UniTask MatchAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Action<T> just,
            Action nothing,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            if (result.IsJust)
            {
                just(result.Value);
            }
            else
            {
                nothing();
            }
        }

        public static async UniTask<TResult> MatchAsync<T, TResult>(
            this UniTask<Maybe<T>> maybe,
            Func<T, TResult> just,
            Func<TResult> nothing,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust ? just(result.Value) : nothing();
        }
        #endregion

        #region OrElseAsync
        public static UniTask<Maybe<T>> OrElseAsync<T>(
            this Maybe<T> maybe,
            Func<CancellationToken, UniTask<Maybe<T>>> getOtherAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return maybe.IsJust ? UniTask.FromResult(maybe) : getOtherAsync(cancellationToken);
        }

        public static async UniTask<Maybe<T>> OrElseAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Func<CancellationToken, UniTask<Maybe<T>>> getOtherAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust ? result : await getOtherAsync(cancellationToken);
        }

        public static async UniTask<Maybe<T>> OrElseAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Maybe<T> other,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust ? result : other;
        }

        public static async UniTask<Maybe<T>> OrElseAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Func<Maybe<T>> getOther,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust ? result : getOther();
        }
        #endregion

        #region EnsureAsync
        public static async UniTask<Maybe<T>> EnsureAsync<T>(
            this Maybe<T> maybe,
            Func<T, CancellationToken, UniTask<bool>> predicateAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (maybe.IsJust)
            {
                return await predicateAsync(maybe.Value, cancellationToken) ? maybe : Maybe.Nothing<T>();
            }

            return maybe;
        }

        public static async UniTask<Maybe<T>> EnsureAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Func<T, CancellationToken, UniTask<bool>> predicateAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            if (result.IsJust)
            {
                return await predicateAsync(result.Value, cancellationToken) ? result : Maybe.Nothing<T>();
            }

            return result;
        }

        public static async UniTask<Maybe<T>> EnsureAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Func<T, bool> predicate,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            if (result.IsJust)
            {
                return predicate(result.Value) ? result : Maybe.Nothing<T>();
            }

            return result;
        }
        #endregion

        #region RecoverAsync
        public static async UniTask<Maybe<T>> RecoverAsync<T>(
            this Maybe<T> maybe,
            Func<CancellationToken, UniTask<T>> getValueAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return maybe.IsJust ? maybe : Maybe.Just(await getValueAsync(cancellationToken));
        }

        public static async UniTask<Maybe<T>> RecoverAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Func<CancellationToken, UniTask<T>> getValueAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust ? result : Maybe.Just(await getValueAsync(cancellationToken));
        }

        public static async UniTask<Maybe<T>> RecoverAsync<T>(
            this UniTask<Maybe<T>> maybe,
            T value,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust ? result : Maybe.Just(value);
        }

        public static async UniTask<Maybe<T>> RecoverAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Func<T> getValue,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust ? result : Maybe.Just(getValue());
        }
        #endregion

        #region GetOrNullAsync
        public static async UniTask<T?> GetOrNullAsync<T>(
            this UniTask<Maybe<T>> maybe,
            CancellationToken cancellationToken = default
        ) where T : class
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust ? result.Value : null;
        }
        #endregion

        #region GetOrElseAsync
        public static UniTask<T> GetOrElseAsync<T>(
            this Maybe<T> maybe,
            Func<CancellationToken, UniTask<T>> getValueAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return maybe.IsJust ? UniTask.FromResult(maybe.Value) : getValueAsync(cancellationToken);
        }

        public static async UniTask<T> GetOrElseAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Func<CancellationToken, UniTask<T>> getValueAsync,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust ? result.Value : await getValueAsync(cancellationToken);
        }

        public static async UniTask<T> GetOrElseAsync<T>(
            this UniTask<Maybe<T>> maybe,
            T value,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust ? result.Value : value;
        }

        public static async UniTask<T> GetOrElseAsync<T>(
            this UniTask<Maybe<T>> maybe,
            Func<T> getValue,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await maybe.AttachExternalCancellation(cancellationToken);

            return result.IsJust ? result.Value : getValue();
        }
        #endregion
    }
}
#endif
