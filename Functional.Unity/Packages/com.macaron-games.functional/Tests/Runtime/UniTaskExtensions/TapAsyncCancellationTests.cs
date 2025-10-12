#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Macaron.Functional;
using Macaron.Functional.UniTaskExtensions;
using NUnit.Framework;

using static Macaron.Functional.Either;
using static Macaron.Functional.Maybe;

namespace Macaron.Functional.UniTaskExtensions.Tests
{
    [TestFixture]
    public class TapAsyncCancellationTests
    {
        [Test]
        public async Task TapAsync_WhenTokenAlreadyCancelled_ThrowsOperationCanceled()
        {
            using var cts = new CancellationTokenSource();
            var source = new UniTaskCompletionSource<Maybe<int>>();

            cts.Cancel();

            var tapTask = source.Task.TapAsync((int _, CancellationToken _) => UniTask.CompletedTask, cts.Token);

            try
            {
                await tapTask;
                Assert.Fail("Expected OperationCanceledException.");
            }
            catch (OperationCanceledException exception)
            {
                Assert.That(exception.CancellationToken, Is.EqualTo(cts.Token));
            }

            source.TrySetResult(Just(1));
        }

        [Test]
        public async Task TapLeftAsync_WhenTokenAlreadyCancelled_ThrowsOperationCanceled()
        {
            using var cts = new CancellationTokenSource();
            var source = new UniTaskCompletionSource<Either<string, int>>();

            cts.Cancel();

            var tapTask = source.Task.TapLeftAsync((_, _) => UniTask.CompletedTask, cts.Token);

            try
            {
                await tapTask;
                Assert.Fail("Expected OperationCanceledException.");
            }
            catch (OperationCanceledException exception)
            {
                Assert.That(exception.CancellationToken, Is.EqualTo(cts.Token));
            }

            source.TrySetResult(Right<string, int>(42));
        }
    }
}
#endif
