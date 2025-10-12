#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

using static Macaron.Functional.Either;

namespace Macaron.Functional.UniTaskExtensions.Tests
{
    [TestFixture]
    public class UtilityRunAsyncTests
    {
        [Test]
        public async Task RunAsync_InvokesDelegateWithToken()
        {
            using var cts = new CancellationTokenSource();
            CancellationToken? observedToken = null;

            await Utility.RunAsync(
                token =>
                {
                    observedToken = token;

                    Assert.That(token, Is.EqualTo(cts.Token));

                    return UniTask.CompletedTask;
                },
                cts.Token
            );

            Assert.That(observedToken, Is.EqualTo(cts.Token));
        }

        [Test]
        public async Task RunAsync_WhenTokenAlreadyCancelled_Throws()
        {
            using var cts = new CancellationTokenSource();

            cts.Cancel();

            var task = Utility.RunAsync(_ => UniTask.CompletedTask, cts.Token);

            try
            {
                await task;

                Assert.Fail("Expected OperationCanceledException.");
            }
            catch (OperationCanceledException exception)
            {
                Assert.That(exception.CancellationToken, Is.EqualTo(cts.Token));
            }
        }

        [Test]
        public async Task RunAsync_WithContext_ReturnsResult()
        {
            var result = await Utility.RunAsync(
                (context, _) =>
                {
                    Assert.That(context, Is.EqualTo("ctx"));

                    return UniTask.FromResult(context.Length);
                },
                "ctx"
            );

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public async Task RunCatchingAsync_ReturnsRightOnSuccess()
        {
            var result = await Utility.RunCatchingAsync(_ => UniTask.CompletedTask);

            Assert.That(result.IsRight, Is.True);
            Assert.That(result.Right, Is.EqualTo(Placeholder._));
        }

        [Test]
        public async Task RunCatchingAsync_ReturnsLeftOnException()
        {
            var result = await Utility.RunCatchingAsync(_ =>
            {
                return UniTask.FromException(new InvalidOperationException("boom"));
            });

            Assert.That(result.IsLeft, Is.True);
            Assert.That(result.Left, Is.TypeOf<InvalidOperationException>());
        }

        [Test]
        public async Task RunCatchingAsync_DoesNotCatchOperationCanceled()
        {
            using var cts = new CancellationTokenSource();

            cts.Cancel();

            var task = Utility.RunCatchingAsync(_ => UniTask.CompletedTask, cts.Token);

            try
            {
                await task;

                Assert.Fail("Expected OperationCanceledException.");
            }
            catch (OperationCanceledException)
            {
            }
        }

        [Test]
        public async Task RunCatchingAsync_WithContext_ReturnsLeftOnException()
        {
            var result = await Utility.RunCatchingAsync<string>(_ => UniTask.FromException<string>(new InvalidOperationException("boom")));

            Assert.That(result.IsLeft, Is.True);
            Assert.That(result.Left, Is.TypeOf<InvalidOperationException>());
        }

        [Test]
        public async Task RunCatchingAsync_WithContext_ReturnsRightOnSuccess()
        {
            var result = await Utility.RunCatchingAsync<string>(
                (context, _) =>
                {
                    Assert.That(context, Is.EqualTo("ctx"));

                    return UniTask.CompletedTask;
                },
                "ctx"
            );

            Assert.That(result, Is.EqualTo(Right<Exception, Placeholder>(Placeholder._)));
        }
    }
}
#endif
