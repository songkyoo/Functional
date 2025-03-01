namespace Macaron.Tests.Functional;

using Macaron.Functional;

[TestFixture]
public class CurryTest
{
    [Test]
    public void Curry_WithFunction_ReturnsCurriedFunction()
    {
        var fn = (int x, int y) => x + y;
        var curriedFn = fn.Curry(); // x => y => fn(x, y);

        Assert.That(curriedFn(1)(2), Is.EqualTo(3));
    }
}
