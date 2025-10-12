#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

using static Macaron.Functional.Maybe;

namespace Macaron.Functional.UniTaskExtensions.Tests
{
    [TestFixture]
    public class MaybeValueTypeExtensionsTests
    {
        [Test]
        public async Task GetOrNullAsync_ReturnsNullableForValueType()
        {
            var source = UniTask.FromResult(Nothing<int>());
            var result = await source.GetOrNullAsync();

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetOrNullAsync_ReturnsValueWhenJust()
        {
            var source = UniTask.FromResult(Just(5));
            var result = await source.GetOrNullAsync();

            Assert.That(result, Is.EqualTo(5));
        }
    }
}
#endif
