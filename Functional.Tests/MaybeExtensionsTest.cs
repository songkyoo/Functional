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

        Assert.That(just.GetOrElse("Bar"), Is.EqualTo("Foo"));
        Assert.That(just.GetOrElse(() => "Bar"), Is.EqualTo("Foo"));
    }

    [Test]
    public void GetOrElse_NothingMaybe_ReturnsReplacementValue()
    {
        Maybe<string> nothing = Nothing();

        Assert.That(nothing.GetOrElse("Bar"), Is.EqualTo("Bar"));
        Assert.That(nothing.GetOrElse(() => "Bar"), Is.EqualTo("Bar"));
    }

    [Test]
    public void OrElse_JustMaybe_ReturnsSelf()
    {
        Maybe<string> just = Just("Foo");

        Assert.That(just.Recover("Bar"), Is.EqualTo(Just("Foo")));
        Assert.That(just.Recover(() => "Bar"), Is.EqualTo(Just("Foo")));
        Assert.That(just.OrElse(Just("Bar")), Is.EqualTo(Just("Foo")));
        Assert.That(just.OrElse(() => Just("Bar")), Is.EqualTo(Just("Foo")));
    }

    [Test]
    public void OrElse_NothingMaybe_ReturnsJustMaybe()
    {
        Maybe<string> nothing = Nothing();

        Assert.That(nothing.Recover("Bar"), Is.EqualTo(Just("Bar")));
        Assert.That(nothing.Recover(() => "Bar"), Is.EqualTo(Just("Bar")));
        Assert.That(nothing.OrElse(Just("Bar")), Is.EqualTo(Just("Bar")));
        Assert.That(nothing.OrElse(() => Just("Bar")), Is.EqualTo(Just("Bar")));
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

        Assert.That(nothing.Ensure(x => x.Length < 2), Is.EqualTo(Nothing()));
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
}
