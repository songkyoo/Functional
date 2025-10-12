#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

namespace Macaron.Functional.UniTaskExtensions.Tests
{
    [TestFixture]
    public class ExtensionsLetIntoAsyncTests
    {
        [Test]
        public async Task LetAsync_ValueFunc_TransformsValue()
        {
            var result = await UniTask.FromResult(4).LetAsync(
                (value, token) =>
                {
                    Assert.That(token.IsCancellationRequested, Is.False);
                    return UniTask.FromResult(value * 2);
                },
                CancellationToken.None
            );

            Assert.That(result, Is.EqualTo(8));
        }

        [Test]
        public async Task LetAsync_Task_WhenCancelledBeforeAwait_Throws()
        {
            using var cts = new CancellationTokenSource();
            var source = new UniTaskCompletionSource<int>();
            var letTask = source.Task.LetAsync((_, _) => UniTask.FromResult(0), cts.Token);

            cts.Cancel();

            try
            {
                await letTask;

                Assert.Fail("Expected OperationCanceledException.");
            }
            catch (OperationCanceledException exception)
            {
                Assert.That(exception.CancellationToken, Is.EqualTo(cts.Token));
            }

            source.TrySetResult(1);
        }

        [Test]
        public async Task LetAsync_TaskFuncWithArgs_UsesArguments()
        {
            var source = UniTask.FromResult(2);
            var result = await source.LetAsync(
                (value, multiplier, token) =>
                {
                    Assert.That(token.IsCancellationRequested, Is.False);
                    return UniTask.FromResult(value * multiplier);
                },
                5,
                CancellationToken.None
            );

            Assert.That(result, Is.EqualTo(10));
        }
    }
}
#endif
