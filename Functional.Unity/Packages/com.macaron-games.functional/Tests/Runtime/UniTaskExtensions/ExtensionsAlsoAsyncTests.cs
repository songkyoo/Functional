#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

namespace Macaron.Functional.UniTaskExtensions.Tests
{
    [TestFixture]
    public class ExtensionsAlsoAsyncTests
    {
        [Test]
        public async Task AlsoAsync_ValueFunc_ExecutesAndReturnsOriginal()
        {
            var calls = new List<int>();
            var result = await UniTask.FromResult(5).AlsoAsync(
                (value, token) =>
                {
                    calls.Add(value);

                    Assert.That(token.IsCancellationRequested, Is.False);

                    return UniTask.CompletedTask;
                },
                CancellationToken.None
            );

            Assert.That(result, Is.EqualTo(5));
            Assert.That(calls, Is.EqualTo(new[] { 5 }));
        }

        [Test]
        public async Task AlsoAsync_Task_WhenCancelledBeforeCompletion_Throws()
        {
            using var cts = new CancellationTokenSource();
            var source = new UniTaskCompletionSource<int>();
            var tapTask = source.Task.AlsoAsync((_, _) => UniTask.CompletedTask, cts.Token);

            cts.Cancel();

            try
            {
                await tapTask;

                Assert.Fail("Expected OperationCanceledException.");
            }
            catch (OperationCanceledException exception)
            {
                Assert.That(exception.CancellationToken, Is.EqualTo(cts.Token));
            }

            source.TrySetResult(1);
        }

        [Test]
        public async Task AlsoAsync_TaskAction_ExecutesActionWithArgs()
        {
            var source = UniTask.FromResult("value");
            var arguments = new List<(string value, int arg)>();
            var result = await source.AlsoAsync(
                (str, arg, token) =>
                {
                    arguments.Add((str, arg));

                    Assert.That(token.IsCancellationRequested, Is.False);

                    return UniTask.CompletedTask;
                },
                123,
                CancellationToken.None
            );

            Assert.That(result, Is.EqualTo("value"));
            Assert.That(arguments, Is.EqualTo(new[] { ("value", 123) }));
        }

        [Test]
        public async Task AlsoAsync_TaskActionWithThreeArgs_UsesAllArguments()
        {
            var source = UniTask.FromResult("value");
            var captured = new List<(string value, int a, int b, int c)>();
            var result = await source.AlsoAsync(
                (str, a, b, c, token) =>
                {
                    captured.Add((str, a, b, c));
                    return UniTask.CompletedTask;
                },
                1,
                2,
                3,
                CancellationToken.None
            );

            Assert.That(result, Is.EqualTo("value"));
            Assert.That(captured, Is.EqualTo(new[] { ("value", 1, 2, 3) }));
        }
    }
}
#endif
