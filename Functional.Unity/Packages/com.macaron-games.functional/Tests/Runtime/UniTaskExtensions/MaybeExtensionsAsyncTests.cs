#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

using static Macaron.Functional.Maybe;

namespace Macaron.Functional.UniTaskExtensions.Tests
{
    [TestFixture]
    public class MaybeExtensionsAsyncTests
    {
        [Test]
        public async Task MapAsync_WithJust_TransformsValue()
        {
            var maybe = Just(5);
            var result = await maybe.MapAsync((value, _) => UniTask.FromResult(value * 2));

            Assert.That(result, Is.EqualTo(Just(10)));
        }

        [Test]
        public async Task MapAsync_TaskWithNothing_ReturnsNothing()
        {
            var source = UniTask.FromResult(Nothing<int>());
            var result = await source.MapAsync((_, _) => UniTask.FromResult(0));

            Assert.That(result.IsNothing, Is.True);
        }

        [Test]
        public async Task FlatMapAsync_WithJust_ReturnsInnerMaybe()
        {
            var maybe = Just("value");
            var result = await maybe.FlatMapAsync((value, _) => UniTask.FromResult(Just(value.Length)));

            Assert.That(result, Is.EqualTo(Just(5)));
        }

        [Test]
        public async Task TapAsync_WithJust_ExecutesAction()
        {
            var values = new List<string>();
            var result = await Just("value").TapAsync((value, _) =>
            {
                values.Add(value);

                return UniTask.CompletedTask;
            });

            Assert.That(result, Is.EqualTo(Just("value")));
            Assert.That(values, Is.EqualTo(new[] { "value" }));
        }

        [Test]
        public async Task TapAsync_WithNothing_DoesNotExecuteAction()
        {
            var values = new List<int>();
            var result = await Nothing<int>().TapAsync((_, _) =>
            {
                values.Add(1);

                return UniTask.CompletedTask;
            });

            Assert.That(result.IsNothing, Is.True);
            Assert.That(values, Is.Empty);
        }

        [Test]
        public async Task TapNothingAsync_WithNothing_ExecutesAction()
        {
            var invoked = false;
            var result = await Nothing<int>().TapNothingAsync(_ =>
            {
                invoked = true;

                return UniTask.CompletedTask;
            });

            Assert.That(result.IsNothing, Is.True);
            Assert.That(invoked, Is.True);
        }

        [Test]
        public async Task MatchAsync_WithJust_InvokesJustBranch()
        {
            var justInvoked = false;
            var nothingInvoked = false;

            await Just(1).MatchAsync(
                justAsync: (value, _) =>
                {
                    justInvoked = true;

                    Assert.That(value, Is.EqualTo(1));

                    return UniTask.CompletedTask;
                },
                nothingAsync: _ =>
                {
                    nothingInvoked = true;

                    return UniTask.CompletedTask;
                });

            Assert.That(justInvoked, Is.True);
            Assert.That(nothingInvoked, Is.False);
        }

        [Test]
        public async Task MatchAsync_Task_ReturnsProjectedValue()
        {
            var source = UniTask.FromResult(Just("value"));
            var result = await source.MatchAsync(
                justAsync: (value, _) => UniTask.FromResult(value.Length),
                nothingAsync: _ => UniTask.FromResult(0)
            );

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public async Task OrElseAsync_WhenNothing_UsesFallback()
        {
            var source = UniTask.FromResult(Nothing<string>());
            var result = await source.OrElseAsync(_ => UniTask.FromResult(Just("fallback")));

            Assert.That(result, Is.EqualTo(Just("fallback")));
        }

        [Test]
        public async Task EnsureAsync_WhenPredicateFails_ReturnsNothing()
        {
            var source = UniTask.FromResult(Just(3));
            var result = await source.EnsureAsync((value, _) => UniTask.FromResult(value > 5));

            Assert.That(result.IsNothing, Is.True);
        }

        [Test]
        public async Task RecoverAsync_WhenNothing_UsesAsyncProvider()
        {
            var source = UniTask.FromResult(Nothing<int>());
            var result = await source.RecoverAsync(_ => UniTask.FromResult(10));

            Assert.That(result, Is.EqualTo(Just(10)));
        }

        [Test]
        public async Task GetOrNullAsync_ReturnsNullForNothing()
        {
            var source = UniTask.FromResult(Nothing<string>());
            var result = await source.GetOrNullAsync();

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetOrElseAsync_WhenNothing_UsesFallbackValue()
        {
            var source = UniTask.FromResult(Nothing<int>());
            var result = await source.GetOrElseAsync(42);

            Assert.That(result, Is.EqualTo(42));
        }
    }
}
#endif
