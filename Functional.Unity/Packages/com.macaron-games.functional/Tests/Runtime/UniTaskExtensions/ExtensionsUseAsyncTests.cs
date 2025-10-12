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
    public class ExtensionsUseAsyncTests
    {
        sealed class TrackingAsyncDisposable : IAsyncDisposable
        {
            public bool Disposed { get; private set; }

            public ValueTask DisposeAsync()
            {
                Disposed = true;

                return default;
            }
        }

        [Test]
        public async Task UseAsync_ValueAction_DisposesAfterAction()
        {
            var disposable = new TrackingAsyncDisposable();
            var invoked = false;

            await disposable.UseAsync(d =>
            {
                invoked = true;

                Assert.That(d, Is.SameAs(disposable));
            });

            Assert.That(invoked, Is.True);
            Assert.That(disposable.Disposed, Is.True);
        }

        [Test]
        public async Task UseAsync_ValueFunc_ReturnsResultAndDisposes()
        {
            var disposable = new TrackingAsyncDisposable();
            var result = await disposable.UseAsync(_ => 42);

            Assert.That(result, Is.EqualTo(42));
            Assert.That(disposable.Disposed, Is.True);
        }

        [Test]
        public async Task UseAsync_ValueAsyncFunc_WithCancelledToken_ThrowsAndDisposes()
        {
            using var cts = new CancellationTokenSource();
            var disposable = new TrackingAsyncDisposable();

            cts.Cancel();

            var task = disposable.UseAsync((_, _) => UniTask.CompletedTask, cts.Token);

            try
            {
                await task;

                Assert.Fail("Expected OperationCanceledException.");
            }
            catch (OperationCanceledException exception)
            {
                Assert.That(exception.CancellationToken, Is.EqualTo(cts.Token));
            }

            Assert.That(disposable.Disposed, Is.True);
        }

        [Test]
        public async Task UseAsync_ValueAsyncFunc_ReturnsResultAndDisposes()
        {
            var disposable = new TrackingAsyncDisposable();
            var result = await disposable.UseAsync((_, _) => UniTask.FromResult(7));

            Assert.That(result, Is.EqualTo(7));
            Assert.That(disposable.Disposed, Is.True);
        }

        [Test]
        public async Task UtilityUseAsync_Action_DisposesAfterAction()
        {
            var disposable = new TrackingAsyncDisposable();
            var invoked = false;

            await Utility.UseAsync(disposable, d =>
            {
                invoked = true;

                Assert.That(d, Is.SameAs(disposable));
            });

            Assert.That(invoked, Is.True);
            Assert.That(disposable.Disposed, Is.True);
        }

        [Test]
        public async Task UtilityUseAsync_Func_ReturnsResultAndDisposes()
        {
            var disposable = new TrackingAsyncDisposable();
            var result = await Utility.UseAsync(disposable, _ => 42);

            Assert.That(result, Is.EqualTo(42));
            Assert.That(disposable.Disposed, Is.True);
        }

        [Test]
        public async Task UtilityUseAsync_WithCancelledToken_ThrowsAndDisposes()
        {
            using var cts = new CancellationTokenSource();
            var disposable = new TrackingAsyncDisposable();

            cts.Cancel();

            var task = Utility.UseAsync(disposable, (_, _) => UniTask.CompletedTask, cts.Token);

            try
            {
                await task;

                Assert.Fail("Expected OperationCanceledException.");
            }
            catch (OperationCanceledException exception)
            {
                Assert.That(exception.CancellationToken, Is.EqualTo(cts.Token));
            }

            Assert.That(disposable.Disposed, Is.True);
        }

        [Test]
        public async Task UtilityUseAsync_ReturnsResultForAsyncFunc()
        {
            var disposable = new TrackingAsyncDisposable();
            var result = await Utility.UseAsync(disposable, (_, _) => UniTask.FromResult(11));

            Assert.That(result, Is.EqualTo(11));
            Assert.That(disposable.Disposed, Is.True);
        }
    }
}
#endif
