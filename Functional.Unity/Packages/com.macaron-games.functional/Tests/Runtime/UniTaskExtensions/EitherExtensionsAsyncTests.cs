#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

using static Macaron.Functional.Either;

namespace Macaron.Functional.UniTaskExtensions.Tests
{
    [TestFixture]
    public class EitherExtensionsAsyncTests
    {
        [Test]
        public async Task MapAsync_WithRight_TransformsValue()
        {
            var either = Right<string, int>(3);
            var result = await either.MapAsync((value, _) => UniTask.FromResult(value * 2));

            Assert.That(result, Is.EqualTo(Right<string, int>(6)));
        }

        [Test]
        public async Task MapAsync_TaskWithLeft_ReturnsLeft()
        {
            var source = UniTask.FromResult(Left<string, int>("error"));
            var result = await source.MapAsync((_, _) => UniTask.FromResult(0));

            Assert.That(result.IsLeft, Is.True);
            Assert.That(result.Left, Is.EqualTo("error"));
        }

        [Test]
        public async Task MapLeftAsync_WithLeft_TransformsValue()
        {
            var either = Left<int, string>(1);
            var result = await either.MapLeftAsync((value, _) => UniTask.FromResult(value + 1));

            Assert.That(result, Is.EqualTo(Left<int, string>(2)));
        }

        [Test]
        public async Task FlatMapAsync_WithRight_UsesContinuation()
        {
            var either = Right<string, int>(2);
            var result = await either.FlatMapAsync((value, _) => UniTask.FromResult(Right<string, int>(value * 3)));

            Assert.That(result, Is.EqualTo(Right<string, int>(6)));
        }

        [Test]
        public async Task TapAsync_WithRight_ExecutesAction()
        {
            var values = new List<int>();
            var result = await Right<string, int>(4).TapAsync((value, _) =>
            {
                values.Add(value);

                return UniTask.CompletedTask;
            });

            Assert.That(result, Is.EqualTo(Right<string, int>(4)));
            Assert.That(values, Is.EqualTo(new[] { 4 }));
        }

        [Test]
        public async Task TapLeftAsync_WithLeft_ExecutesAction()
        {
            var values = new List<string>();
            var result = await Left<string, int>("err").TapLeftAsync((value, _) =>
            {
                values.Add(value);

                return UniTask.CompletedTask;
            });

            Assert.That(result, Is.EqualTo(Left<string, int>("err")));
            Assert.That(values, Is.EqualTo(new[] { "err" }));
        }

        [Test]
        public async Task MatchAsync_WithRight_InvokesRightBranch()
        {
            var leftInvoked = false;
            var rightInvoked = false;

            await Right<string, int>(5).MatchAsync(
                leftAsync: (_, _) =>
                {
                    leftInvoked = true;

                    return UniTask.CompletedTask;
                },
                rightAsync: (value, _) =>
                {
                    rightInvoked = true;
                    Assert.That(value, Is.EqualTo(5));

                    return UniTask.CompletedTask;
                });

            Assert.That(leftInvoked, Is.False);
            Assert.That(rightInvoked, Is.True);
        }

        [Test]
        public async Task OrElseAsync_WithLeft_UsesFallback()
        {
            var source = UniTask.FromResult(Left<string, int>("err"));
            var result = await source.OrElseAsync(_ => UniTask.FromResult(Right<string, int>(42)));

            Assert.That(result, Is.EqualTo(Right<string, int>(42)));
        }

        [Test]
        public async Task EnsureAsync_WhenPredicateFails_ReturnsLeft()
        {
            var source = UniTask.FromResult(Right<string, int>(3));
            var result = await source.EnsureAsync((value, _) => UniTask.FromResult(value > 5), "too small");

            Assert.That(result.IsLeft, Is.True);
            Assert.That(result.Left, Is.EqualTo("too small"));
        }

        [Test]
        public async Task GetOrElseAsync_WithLeft_ReturnsFallbackValue()
        {
            var source = UniTask.FromResult(Left<string, int>("err"));
            var result = await source.GetOrElseAsync(10);

            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public async Task GetLeftOrElseAsync_WithRight_ReturnsFallback()
        {
            var source = UniTask.FromResult(Right<string, int>(5));
            var result = await source.GetLeftOrElseAsync("fallback");

            Assert.That(result, Is.EqualTo("fallback"));
        }

        [Test]
        public async Task GetOrNullAsync_ReturnsNullWhenLeft()
        {
            var source = UniTask.FromResult(Left<string, string>("error"));
            var result = await source.GetOrNullAsync();

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetLeftOrNullAsync_ReturnsNullWhenRight()
        {
            var source = UniTask.FromResult(Right<string, string>("value"));
            var result = await source.GetLeftOrNullAsync();

            Assert.That(result, Is.Null);
        }
    }
}
#endif
