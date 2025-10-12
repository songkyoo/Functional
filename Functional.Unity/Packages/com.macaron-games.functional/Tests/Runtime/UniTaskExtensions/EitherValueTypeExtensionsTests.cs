#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

using static Macaron.Functional.Either;

namespace Macaron.Functional.UniTaskExtensions.Tests
{
    [TestFixture]
    public class EitherValueTypeExtensionsTests
    {
        [Test]
        public async Task GetOrNullAsync_ReturnsNullWhenLeft()
        {
            var source = UniTask.FromResult(Left<string, int>("error"));
            var result = await source.GetOrNullAsync();

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetOrNullAsync_ReturnsValueWhenRight()
        {
            var source = UniTask.FromResult(Right<string, int>(5));
            var result = await source.GetOrNullAsync();

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public async Task GetLeftOrNullAsync_ReturnsValueWhenLeft()
        {
            var source = UniTask.FromResult(Left<int, string>(1));
            var result = await source.GetLeftOrNullAsync();

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task GetLeftOrNullAsync_ReturnsNullWhenRight()
        {
            var source = UniTask.FromResult(Right<int, string>("value"));
            var result = await source.GetLeftOrNullAsync();

            Assert.That(result, Is.Null);
        }
    }
}
#endif
