using static Macaron.Functional.Utility;

using Is = NUnit.Framework.Is;

namespace Macaron.Functional.Tests;

[TestFixture]
public class PipeTest
{
    [Test]
    public void Pipe_AppliesFunctionsLeftToRight()
    {
        var fn = Pipe(
            (int x) => x + 1,
            (int x) => x * 2,
            (int x) => x.ToString()
        );

        Assert.That(fn(2), Is.EqualTo("6"));
    }
}
