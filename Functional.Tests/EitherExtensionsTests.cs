using static Macaron.Functional.Either;

namespace Macaron.Functional.Tests;

[TestFixture]
public class EitherExtensionsTest
{
    [Test]
    public void GetOrNull_Right_ReturnsRightValue()
    {
        var right = Right<string, int>(42);
        Assert.That(right.GetOrNull(), Is.EqualTo(42));
    }

    [Test]
    public void GetOrNull_ValueTypeLeft_ReturnsNull()
    {
        var left = Left<int, string>(42);
        Assert.That(left.GetOrNull(), Is.Null);
    }

    [Test]
    public void GetOrNull_Left_ReturnsNull()
    {
        var left = Left<string, int>("Foo");
        Assert.That(left.GetOrNull(), Is.Null);
    }

    [Test]
    public void GetLeftOrNull_Left_ReturnsLeftValue()
    {
        var left = Left<int, string>(42);
        Assert.That(left.GetLeftOrNull(), Is.EqualTo(42));
    }

    [Test]
    public void GetLeftOrNull_ValueTypeRight_ReturnsNull()
    {
        var right = Right<string, int>(42);
        Assert.That(right.GetLeftOrNull(), Is.Null);
    }

    [Test]
    public void GetLeftOrNull_Right_ReturnsNull()
    {
        var right = Right<int, string>("Foo");
        Assert.That(right.GetLeftOrNull(), Is.Null);
    }

    [Test]
    public void GetOrThrow_Right_ReturnsRightValue()
    {
        var right = Right<Exception, string>("Foo");
        Assert.That(right.GetOrThrow(), Is.EqualTo("Foo"));
    }

    [Test]
    public void GetOrThrow_Left_Throws()
    {
        var exception = new InvalidOperationException("Foo");
        var left = Left<Exception, string>(exception);

        Assert.Throws<InvalidOperationException>(() => left.GetOrThrow());
    }

    [Test]
    public void GetOrElse_Right_ReturnsRight()
    {
        Either<string, string> right = Right("Foo");

        Assert.That(right.GetOrElse("Bar"), Is.EqualTo("Foo"));
        Assert.That(right.GetOrElse(() => "Bar"), Is.EqualTo("Foo"));
        Assert.That(right.GetOrElse(left => "Bar"), Is.EqualTo("Foo"));
    }

    [Test]
    public void GetOrElse_Left_ReturnsRightValue()
    {
        Either<string, string> left = Left("Foo");

        Assert.That(left.GetOrElse("Bar"), Is.EqualTo("Bar"));
        Assert.That(left.GetOrElse(() => "Bar"), Is.EqualTo("Bar"));
        Assert.That(left.GetOrElse(left => left), Is.EqualTo("Foo"));
    }

    [Test]
    public void OrElse_Right_ReturnsSelf()
    {
        Either<string, string> right = Right("Foo");

        Assert.That(right.OrElse("Bar"), Is.EqualTo(Right("Foo")));
        Assert.That(right.OrElse(Right("Bar")), Is.EqualTo(Right("Foo")));
        Assert.That(right.OrElse(() => Right("Bar")), Is.EqualTo(Right("Foo")));
        Assert.That(right.OrElse(left => Right("Bar")), Is.EqualTo(Right("Foo")));
    }

    [Test]
    public void OrElse_Left_ReturnsRight()
    {
        Either<string, string> left = Left("Foo");

        Assert.That(left.OrElse("Bar"), Is.EqualTo(Right("Bar")));
        Assert.That(left.OrElse(Right("Bar")), Is.EqualTo(Right("Bar")));
        Assert.That(left.OrElse(() => Right("Bar")), Is.EqualTo(Right("Bar")));
        Assert.That(left.OrElse(left => Right(left)), Is.EqualTo(Right("Foo")));
    }

    [Test]
    public void Ensure_ValueSatisfiesPredicate_ReturnsSelf()
    {
        var right = Right<string, int>(42);
        var result = right.Ensure(x => x > 0, "fail");

        Assert.That(result, Is.EqualTo(right));
    }

    [Test]
    public void Ensure_ValueFailsPredicate_ReturnsLeft()
    {
        var right = Right<string, int>(0);
        var result = right.Ensure(x => x > 0, "fail");

        Assert.That(result, Is.EqualTo(Left<string, int>("fail")));
    }

    [Test]
    public void Ensure_Left_ReturnsSelf()
    {
        var left = Left<string, int>("error");
        var result = left.Ensure(x => true, "ignored");

        Assert.That(result, Is.EqualTo(left));
    }

    [Test]
    public void Ensure_WithFuncLeftValueFailsPredicate_ReturnsLeft()
    {
        var right = Right<string, int>(42);
        var result = right.Ensure(x => false, () => "generated");

        Assert.That(result, Is.EqualTo(Left<string, int>("generated")));
    }

    [Test]
    public void Ensure_WithFuncRightLeftValueFailsPredicate_ReturnsLeft()
    {
        var right = Right<string, int>(7);
        var result = right.Ensure(x => x % 2 == 0, x => $"odd:{x}");

        Assert.That(result, Is.EqualTo(Left<string, int>("odd:7")));
    }

    [Test]
    public void Ensure_WithReplacementEither_ReturnsReplacementOnFailure()
    {
        var right = Right<string, int>(0);
        var replacement = Right<string, int>(42);
        var result = right.Ensure(x => x > 0, replacement);

        Assert.That(result, Is.EqualTo(replacement));
    }

    [Test]
    public void Ensure_WithFuncReplacementEither_ReturnsReplacementOnFailure()
    {
        var right = Right<string, int>(7);
        var result = right.Ensure(x => false, () => Right<string, int>(42));

        Assert.That(result, Is.EqualTo(Right<string, int>(42)));
    }

    [Test]
    public void Ensure_WithFuncRightReplacementEither_ReturnsReplacementOnFailure()
    {
        var right = Right<string, int>(7);
        var result = right.Ensure(x => false, x => Right<string, int>(x * 2));

        Assert.That(result, Is.EqualTo(Right<string, int>(14)));
    }

    [Test]
    public void Ensure_WithFuncRightReplacementEither_PredicateTrue_ReturnsSelf()
    {
        var right = Right<string, int>(42);
        var result = right.Ensure(x => true, x => Right<string, int>(x * 10));

        Assert.That(result, Is.EqualTo(right));
    }

    [Test]
    public void TryGet_Right_ReturnsTrueAndRightValue()
    {
        var right = Right<string, int>(42);
        var result = right.TryGet(out var value);

        Assert.That(result, Is.True);
        Assert.That(value, Is.EqualTo(42));
    }

    [Test]
    public void TryGet_Left_ReturnsFalseAndDefault()
    {
        var left = Left<string, int>("Foo");
        var result = left.TryGet(out var value);

        Assert.That(result, Is.False);
        Assert.That(value, Is.EqualTo(0));
    }

    [Test]
    public void TryGetLeft_Left_ReturnsTrueAndValue()
    {
        var left = Left<string, int>("Foo");
        var result = left.TryGetLeft(out var value);

        Assert.That(result, Is.True);
        Assert.That(value, Is.EqualTo("Foo"));
    }

    [Test]
    public void TryGetLeft_Right_ReturnsFalseAndDefault()
    {
        var right = Right<string, int>(42);
        var result = right.TryGetLeft(out var value);

        Assert.That(result, Is.False);
        Assert.That(value, Is.Null);
    }

    [Test]
    public void Apply_BothRight_AppliesFunction()
    {
        var fn = Right<string, Func<int, string>>(x => $"Answer: {x}");
        var arg = Right<string, int>(42);
        var result = fn.Apply(arg);

        Assert.That(result.Right, Is.EqualTo("Answer: 42"));
    }

    [Test]
    public void Apply_FunctionLeft_ReturnsLeft()
    {
        var fn = Left<string, Func<int, string>>("fail");
        var arg = Right<string, int>(42);
        var result = fn.Apply(arg);

        Assert.That(result.IsLeft, Is.True);
    }
}
