using static Macaron.Functional.Maybe;

namespace Macaron.Functional.Tests;

[TestFixture]
public class MaybeTest
{
    [Test]
    public void DefaultCtor_WithoutJustOrNothing_CreateInvalidMaybeObject()
    {
        Maybe<string> maybe = default;

        Assert.That(() => maybe.IsJust, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(() => maybe.IsNothing, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(() => maybe.Value, Throws.Exception.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void Just_WithValue_ReturnsJustMaybe()
    {
        Maybe<string> just = Just("Foo");

        Assert.That(just.IsJust, Is.True);
        Assert.That(just.IsNothing, Is.False);
        Assert.That(just.Value, Is.EqualTo("Foo"));
    }

    [Test]
    public void Nothing_WithoutType_ReturnsNothingMaybe()
    {
        Maybe<string> nothing = Nothing();

        Assert.That(nothing.IsJust, Is.False);
        Assert.That(nothing.IsNothing, Is.True);
        Assert.That(() => nothing.Value, Throws.Exception.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void Nothing_WithType_ReturnsNothingMaybe()
    {
        Maybe<string> nothing = Nothing<string>();

        Assert.That(nothing.IsJust, Is.False);
        Assert.That(nothing.IsNothing, Is.True);
        Assert.That(() => nothing.Value, Throws.Exception.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void Map_JustMaybe_ReturnsJustMaybe()
    {
        Maybe<string> just = Just("Foo");
        Func<string, string> toUpper = str => str.ToUpper();

        Assert.That(just.Map(toUpper), Is.EqualTo(Just("FOO")));
    }

    [Test]
    public void Map_NothingMaybe_ReturnsNothingMaybe()
    {
        Maybe<string> nothing = Nothing();
        Func<string, string> toUpper = str => str.ToUpper();

        Assert.That(nothing.Map(toUpper), Is.EqualTo(Nothing()));
    }

    [Test]
    public void FlatMap_JustMaybeReturnedByFuncWithJustMaybe_ReturnsReturnedJustMaybe()
    {
        Maybe<string> just = Just("Foo");
        Func<string, Maybe<string>> toUpper = str => Just(str.ToUpper());

        Assert.That(just.FlatMap(toUpper), Is.EqualTo(Just("FOO")));
    }

    [Test]
    public void FlatMap_NothingMaybeReturnedByFuncWithJustMaybe_ReturnsReturnedNothingMaybe()
    {
        Maybe<string> just = Just("Foo");
        Func<string, Maybe<string>> toNothing = _ => Nothing();

        Assert.That(just.FlatMap(toNothing), Is.EqualTo(Nothing()));
    }

    [Test]
    public void FlatMap_NothingMaybe_ReturnsNothingMaybe()
    {
        Maybe<string> nothing = Nothing();
        Func<string, Maybe<string>> toUpper = str => Just(str.ToUpper());

        Assert.That(nothing.FlatMap(toUpper), Is.EqualTo(Nothing()));
    }

    [Test]
    public void Match_JustMaybe_ReturnsJustValue()
    {
        Maybe<string> just = Just("Foo");

        Assert.That(
            actual: just.Match(
                just: value => value,
                nothing: () => "Nothing"
            ),
            expression: Is.EqualTo("Foo")
        );
    }

    [Test]
    public void Match_NothingMaybe_ReturnsNothingValue()
    {
        Maybe<string> nothing = Nothing();

        Assert.That(
            actual: nothing.Match(
                just: value => value,
                nothing: () => "Nothing"
            ),
            expression: Is.EqualTo("Nothing")
        );
    }

    [Test]
    public void Match_JustMaybe_ExecutesJustAction()
    {
        Maybe<string> just = Just("Foo");

        var executed = false;
        just.Match(just: _ => executed = true, nothing: () => { });

        Assert.That(executed, Is.True);
    }

    [Test]
    public void Match_NothingMaybe_ExecutesNothingAction()
    {
        Maybe<string> nothing = Nothing();

        var executed = false;
        nothing.Match(just: _ => { }, nothing: () => executed = true);

        Assert.That(executed, Is.True);
    }

    [Test]
    public void From_PredicateReturnsTrueWithNullValue_ReturnsJust()
    {
        var maybe = From<object?>(null, value => true);

        Assert.That(maybe.IsJust, Is.True);
        Assert.That(maybe.Value, Is.Null);
    }

    [Test]
    public void From_PredicateReturnsFalse_ReturnsNothing()
    {
        var maybe = From("value", value => value.Length == 0);

        Assert.That(maybe.IsNothing, Is.True);
    }

    [Test]
    public void Of_ReferenceValue_ReturnsJust()
    {
        var maybe = Of("value");

        Assert.That(maybe.IsJust, Is.True);
        Assert.That(maybe.Value, Is.EqualTo("value"));
    }

    [Test]
    public void Of_NullReference_ReturnsNothing()
    {
        var maybe = Of<string>(null);

        Assert.That(maybe.IsNothing, Is.True);
    }

    [Test]
    public void Of_ValueType_ReturnsJust()
    {
        var maybe = Of<int>(7);

        Assert.That(maybe.IsJust, Is.True);
        Assert.That(maybe.Value, Is.EqualTo(7));
    }

    [Test]
    public void Of_NullValueType_ReturnsNothing()
    {
        int? nullable = null;
        var maybe = Of(nullable);

        Assert.That(maybe.IsNothing, Is.True);
    }
}
