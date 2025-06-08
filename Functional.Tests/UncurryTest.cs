namespace Macaron.Functional.Tests;

[TestFixture]
public class UncurryTest
{
    [Test]
    public void Uncurry_WithCurriedFunction_ReturnsUncurriedFunction()
    {
        var fn = (int x, int y, int z) => x + y + z;
        var curriedFn = fn.Curry(); // x => y => z => fn(x, y, z);

        var oneAppliedFn = curriedFn(1); // y => z => fn(1, y, z);
        var uncurriedFn = oneAppliedFn.Uncurry(); // (int x, int y) => fn(x, y, z);

        Assert.That(uncurriedFn(2, 3), Is.EqualTo(6));
    }
}
