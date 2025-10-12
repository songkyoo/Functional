#if MACARON_FUNCTIONAL_UNITASK
#nullable enable

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

namespace Macaron.Functional.UniTaskExtensions.Tests
{
    [TestFixture]
    public class ExtensionsIntoAsyncTests
    {
        [Test]
        public async Task IntoAsync_ValueFunc_ReordersArguments()
        {
            var captured = new List<(int prefix, string value)>();

            var result = await UniTask.FromResult("world").IntoAsync((prefix, str, token) =>
            {
                captured.Add((prefix, str));
                return UniTask.FromResult($"{prefix}:{str}");
            }, 42, CancellationToken.None);

            Assert.That(result, Is.EqualTo("42:world"));
            Assert.That(captured, Is.EqualTo(new[] { (42, "world") }));
        }

        [Test]
        public async Task IntoAsync_TaskFuncWithThreeArgs_UsesAllArguments()
        {
            var source = UniTask.FromResult("value");

            var result = await source.IntoAsync((int a, int b, int c, string str, CancellationToken token) =>
            {
                Assert.That(str, Is.EqualTo("value"));
                return UniTask.FromResult($"{a + b + c}:{str}");
            }, 1, 2, 3, CancellationToken.None);

            Assert.That(result, Is.EqualTo("6:value"));
        }
    }
}
#endif
