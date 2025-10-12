using static Macaron.Functional.Either;
using static Macaron.Functional.Maybe;

namespace Macaron.Functional.Tests;

[TestFixture]
public class MaybeExtensionsTest
{
    [Test]
    public void GetOrNull_JustMaybe_ReturnsMaybeValue()
    {
        Maybe<string> just = Just("Foo");

        Assert.That(just.GetOrNull(), Is.EqualTo("Foo"));
    }

    [Test]
    public void GetOrNull_ValueTypeNothingMaybe_ReturnsNull()
    {
        Maybe<int> nothing = Nothing();

        Assert.That(nothing.GetOrNull(), Is.Null);
    }

    [Test]
    public void GetOrNull_NothingMaybe_ReturnsNull()
    {
        Maybe<string> nothing = Nothing();

        Assert.That(nothing.GetOrNull(), Is.Null);
    }

    [Test]
    public void GetOrElse_JustMaybe_ReturnsMaybeValue()
    {
        Maybe<string> just = Just("Foo");
        var fallbackInvoked = false;

        Assert.That(just.GetOrElse("Bar"), Is.EqualTo("Foo"));
        Assert.That(just.GetOrElse(() =>
        {
            fallbackInvoked = true;

            return "Bar";
        }), Is.EqualTo("Foo"));
        Assert.That(fallbackInvoked, Is.False);
    }

    [Test]
    public void GetOrElse_NothingMaybe_ReturnsReplacementValue()
    {
        Maybe<string> nothing = Nothing();
        var fallbackCalls = 0;

        Assert.That(nothing.GetOrElse("Bar"), Is.EqualTo("Bar"));
        Assert.That(nothing.GetOrElse(() =>
        {
            fallbackCalls++;

            return "Bar";
        }), Is.EqualTo("Bar"));
        Assert.That(fallbackCalls, Is.EqualTo(1));
    }

    [Test]
    public void OrElse_JustMaybe_ReturnsSelf()
    {
        Maybe<string> just = Just("Foo");
        var recoverInvoked = false;
        var orElseInvoked = false;

        Assert.That(just.Recover("Bar"), Is.EqualTo(Just("Foo")));
        Assert.That(just.Recover(() =>
        {
            recoverInvoked = true;

            return "Bar";
        }), Is.EqualTo(Just("Foo")));
        Assert.That(just.OrElse(Just("Bar")), Is.EqualTo(Just("Foo")));
        Assert.That(just.OrElse(() =>
        {
            orElseInvoked = true;

            return Just("Bar");
        }), Is.EqualTo(Just("Foo")));
        Assert.That(recoverInvoked, Is.False);
        Assert.That(orElseInvoked, Is.False);
    }

    [Test]
    public void OrElse_NothingMaybe_ReturnsJustMaybe()
    {
        Maybe<string> nothing = Nothing();
        var recoverCalls = 0;
        var orElseCalls = 0;

        Assert.That(nothing.Recover("Bar"), Is.EqualTo(Just("Bar")));
        Assert.That(nothing.Recover(() =>
        {
            recoverCalls++;

            return "Bar";
        }), Is.EqualTo(Just("Bar")));
        Assert.That(nothing.OrElse(Just("Bar")), Is.EqualTo(Just("Bar")));
        Assert.That(nothing.OrElse(() =>
        {
            orElseCalls++;

            return Just("Bar");
        }), Is.EqualTo(Just("Bar")));
        Assert.That(recoverCalls, Is.EqualTo(1));
        Assert.That(orElseCalls, Is.EqualTo(1));
    }

    [Test]
    public void Ensure_ValueSatisfiesPredicate_ReturnsSelf()
    {
        Maybe<string> maybe = Just("Foo");

        Assert.That(maybe.Ensure(x => x.Length > 2), Is.EqualTo(Just("Foo")));
    }

    [Test]
    public void Ensure_ValueFailsPredicate_ReturnsNothing()
    {
        Maybe<string> just = Just("Foo");

        Assert.That(just.Ensure(x => x.Length < 2), Is.EqualTo(Nothing()));
    }

    [Test]
    public void Ensure_Nothing_ReturnsSelf()
    {
        Maybe<string> nothing = Nothing();
        var predicateInvoked = false;

        Assert.That(nothing.Ensure(x =>
        {
            predicateInvoked = true;

            return x.Length < 2;
        }), Is.EqualTo(Nothing()));
        Assert.That(predicateInvoked, Is.False);
    }

    [Test]
    public void TryGet_Just_ReturnsTrueAndJustValue()
    {
        var just = Just(42);
        var result = just.TryGet(out var value);

        Assert.That(result, Is.True);
        Assert.That(value, Is.EqualTo(42));
    }

    [Test]
    public void TryGet_Nothing_ReturnsFalseAndDefault()
    {
        var nothing = Nothing<int>();
        var result = nothing.TryGet(out var value);

        Assert.That(result, Is.False);
        Assert.That(value, Is.EqualTo(0));
    }

    [Test]
    public void Apply_BothJust_AppliesFunction()
    {
        var fn = Just<Func<int, string>>(x => $"Answer: {x}");
        var arg = Just(42);
        var result = fn.Apply(arg);

        Assert.That(result.Value, Is.EqualTo("Answer: 42"));
    }

    [Test]
    public void Apply_FunctionNothing_ReturnsNothing()
    {
        var fn = Just<Func<int, string>>(x => $"Answer: {x}");
        var arg = Nothing<int>();
        var result = fn.Apply(arg);

        Assert.That(result.IsNothing, Is.True);
    }

    [Test]
    public void Recover_JustMaybe_DoesNotInvokeFactory()
    {
        var just = Just("value");
        var invoked = false;
        var result = just.Recover(() =>
        {
            invoked = true;

            return "fallback";
        });

        Assert.That(result, Is.EqualTo(Just("value")));
        Assert.That(invoked, Is.False);
    }

    [Test]
    public void Recover_NothingMaybe_InvokesFactoryOnce()
    {
        var nothing = Nothing<string>();
        var calls = 0;
        var result = nothing.Recover(() =>
        {
            calls++;

            return "fallback";
        });

        Assert.That(result, Is.EqualTo(Just("fallback")));
        Assert.That(calls, Is.EqualTo(1));
    }

    [Test]
    public void ToEither_JustMaybe_ReturnsRight()
    {
        var just = Just("value");

        Assert.That(just.ToEither("left"), Is.EqualTo(Right<string, string>("value")));
        Assert.That(just.ToEither(() => "left"), Is.EqualTo(Right<string, string>("value")));
    }

    [Test]
    public void ToEither_NothingMaybe_ReturnsLeft()
    {
        var nothing = Nothing<string>();
        var calls = 0;

        Assert.That(nothing.ToEither("left"), Is.EqualTo(Left<string, string>("left")));
        Assert.That(nothing.ToEither(() =>
        {
            calls++;

            return "left";
        }), Is.EqualTo(Left<string, string>("left")));
        Assert.That(calls, Is.EqualTo(1));
    }
}
