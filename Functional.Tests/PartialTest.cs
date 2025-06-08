using static Macaron.Functional.Placeholder;

namespace Macaron.Functional.Tests;

[TestFixture]
public class PartialTest
{
    [Test]
    public void Partial_WithPlaceholder_ReturnsPartialAppliedFunction()
    {
        var fn = (int x, int y) => x + y;
        var add1 = fn.Partial(_, 1); // x => fn(x, 1);

        Assert.That(add1(2), Is.EqualTo(3));
    }
}
