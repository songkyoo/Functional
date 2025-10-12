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
    public void GetOrElse_Right_ReturnsRight()
    {
        Either<string, string> right = Right("Foo");
        var valueFactoryInvoked = false;

        Assert.That(right.GetOrElse("Bar"), Is.EqualTo("Foo"));
        Assert.That(right.GetOrElse(_ =>
        {
            valueFactoryInvoked = true;

            return "Bar";
        }), Is.EqualTo("Foo"));
        Assert.That(right.GetOrElse(left =>
        {
            valueFactoryInvoked = true;

            return "Bar";
        }), Is.EqualTo("Foo"));
        Assert.That(valueFactoryInvoked, Is.False);
    }

    [Test]
    public void GetOrElse_Left_ReturnsRightValue()
    {
        Either<string, string> left = Left("Foo");
        var valueFactoryCalls = 0;

        Assert.That(left.GetOrElse("Bar"), Is.EqualTo("Bar"));
        Assert.That(left.GetOrElse(_ =>
        {
            valueFactoryCalls++;

            return "Bar";
        }), Is.EqualTo("Bar"));
        Assert.That(left.GetOrElse(left2 =>
        {
            valueFactoryCalls++;

            return left2;
        }), Is.EqualTo("Foo"));
        Assert.That(valueFactoryCalls, Is.EqualTo(2));
    }

    [Test]
    public void OrElse_Right_ReturnsSelf()
    {
        Either<string, string> right = Right("Foo");
        var recoverInvoked = false;
        var orElseInvoked = false;

        Assert.That(right.Recover("Bar"), Is.EqualTo(Right("Foo")));
        Assert.That(right.Recover(_ =>
        {
            recoverInvoked = true;

            return "Bar";
        }), Is.EqualTo(Right("Foo")));
        Assert.That(right.OrElse(Right("Bar")), Is.EqualTo(Right("Foo")));
        Assert.That(right.OrElse(_ =>
        {
            orElseInvoked = true;

            return Right("Bar");
        }), Is.EqualTo(Right("Foo")));
        Assert.That(right.OrElse(left =>
        {
            orElseInvoked = true;

            return Right("Bar");
        }), Is.EqualTo(Right("Foo")));
        Assert.That(recoverInvoked, Is.False);
        Assert.That(orElseInvoked, Is.False);
    }

    [Test]
    public void OrElse_Left_ReturnsRight()
    {
        Either<string, string> left = Left("Foo");
        var recoverCalls = 0;
        var orElseCalls = 0;

        Assert.That(left.Recover("Bar"), Is.EqualTo(Right("Bar")));
        Assert.That(left.Recover(_ =>
        {
            recoverCalls++;

            return "Bar";
        }), Is.EqualTo(Right("Bar")));
        Assert.That(left.OrElse(Right("Bar")), Is.EqualTo(Right("Bar")));
        Assert.That(left.OrElse(_ =>
        {
            orElseCalls++;

            return Right("Bar");
        }), Is.EqualTo(Right("Bar")));
        Assert.That(left.OrElse(left2 =>
        {
            orElseCalls++;

            return Right(left2);
        }), Is.EqualTo(Right("Foo")));
        Assert.That(recoverCalls, Is.EqualTo(1));
        Assert.That(orElseCalls, Is.EqualTo(2));
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
        var result = right.Ensure(x => false, _ => "generated");

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
    public void GetLeftOrElse_Left_ReturnsLeftWithoutInvokingFallback()
    {
        var left = Left<string, int>("err");
        var fallbackInvoked = false;

        Assert.That(left.GetLeftOrElse("fallback"), Is.EqualTo("err"));
        Assert.That(left.GetLeftOrElse(_ =>
        {
            fallbackInvoked = true;

            return "fallback";
        }), Is.EqualTo("err"));
        Assert.That(fallbackInvoked, Is.False);
    }

    [Test]
    public void GetLeftOrElse_Right_UsesRightValue()
    {
        var right = Right<string, int>(7);
        var calls = 0;

        Assert.That(right.GetLeftOrElse("fallback"), Is.EqualTo("fallback"));
        Assert.That(right.GetLeftOrElse(value =>
        {
            calls++;

            return $"from:{value}";
        }), Is.EqualTo("from:7"));
        Assert.That(calls, Is.EqualTo(1));
    }

    [Test]
    public void Recover_Right_DoesNotInvokeFactory()
    {
        var right = Right<string, int>(7);
        var invoked = false;
        var result = right.Recover(_ =>
        {
            invoked = true;

            return 10;
        });

        Assert.That(result, Is.EqualTo(right));
        Assert.That(invoked, Is.False);
    }

    [Test]
    public void Recover_Left_UsesFactoryValue()
    {
        var left = Left<string, int>("error");
        var result = left.Recover(value => value.Length);

        Assert.That(result, Is.EqualTo(Right<string, int>(5)));
    }

    [Test]
    public void ToMaybe_Right_ReturnsJust()
    {
        var right = Right<string, int>(42);

        Assert.That(right.ToMaybe(), Is.EqualTo(Maybe.Just(42)));
    }

    [Test]
    public void ToMaybe_Left_ReturnsNothing()
    {
        var left = Left<string, int>("error");

        Assert.That(left.ToMaybe(), Is.EqualTo(Maybe.Nothing<int>()));
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
