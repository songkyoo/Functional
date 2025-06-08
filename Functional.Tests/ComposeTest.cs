using static Macaron.Functional.Utility;

namespace Macaron.Functional.Tests;

[TestFixture]
public class ComposeTest
{
    [Test]
    public void Compose_AppliesFunctionsRightToLeft()
    {
        var fn = Compose(
            (int x) => x.ToString(),
            (int x) => x + 1,
            (int x) => x * 2
        );

        Assert.That(fn(2), Is.EqualTo("5"));
    }
}
