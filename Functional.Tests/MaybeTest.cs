using static Macaron.Functional.Maybe;

namespace Macaron.Functional.Tests;

[TestFixture]
public class MaybeTest
{
    [Test]
    public void DefaultCtor_WithoutJustOrNothing_CreateInvalidMaybeObject()
    {
        Maybe<string> option = default;

        Assert.That(() => option.IsJust, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(() => option.IsNothing, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(() => option.Value, Throws.Exception.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void Just_WithValue_ReturnsJustMaybe()
    {
        Maybe<string> option = Just("Foo");

        Assert.That(option.IsJust, Is.True);
        Assert.That(option.IsNothing, Is.False);
        Assert.That(option.Value, Is.EqualTo("Foo"));
    }

    [Test]
    public void Nothing_WithoutType_ReturnsNothingMaybe()
    {
        Maybe<string> option = Nothing();

        Assert.That(option.IsJust, Is.False);
        Assert.That(option.IsNothing, Is.True);
        Assert.That(() => option.Value, Throws.Exception.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void Nothing_WithType_ReturnsNothingMaybe()
    {
        Maybe<string> option = Nothing<string>();

        Assert.That(option.IsJust, Is.False);
        Assert.That(option.IsNothing, Is.True);
        Assert.That(() => option.Value, Throws.Exception.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void GetOrElse_JustMaybe_ReturnsMaybeValue()
    {
        Maybe<string> option = Just("Foo");

        Assert.That(option.GetOrElse("Bar"), Is.EqualTo("Foo"));
    }

    [Test]
    public void GetOrElse_NothingMaybe_ReturnsReplacementValue()
    {
        Maybe<string> option = Nothing();

        Assert.That(option.GetOrElse("Bar"), Is.EqualTo("Bar"));
    }

    [Test]
    public void OrElse_JustMaybe_ReturnsSelf()
    {
        Maybe<string> option = Just("Foo");

        Assert.That(option.OrElse("Bar"), Is.EqualTo(Just("Foo")));
    }

    [Test]
    public void OrElse_NothingMaybe_ReturnsJustMaybe()
    {
        Maybe<string> option = Nothing();

        Assert.That(option.OrElse("Bar"), Is.EqualTo(Just("Bar")));
    }

    [Test]
    public void Map_JustMaybe_ReturnsJustMaybe()
    {
        Maybe<string> option = Just("Foo");
        Func<string, string> toUpper = str => str.ToUpper();

        Assert.That(option.Map(toUpper), Is.EqualTo(Just("FOO")));
    }

    [Test]
    public void Map_NothingMaybe_ReturnsNothingMaybe()
    {
        Maybe<string> option = Nothing();
        Func<string, string> toUpper = str => str.ToUpper();

        Assert.That(option.Map(toUpper), Is.EqualTo(Nothing()));
    }

    [Test]
    public void FlatMap_JustMaybeReturnedByFuncWithJustMaybe_ReturnsReturnedJustMaybe()
    {
        Maybe<string> option = Just("Foo");
        Func<string, Maybe<string>> toUpper = str => Just(str.ToUpper());

        Assert.That(option.FlatMap(toUpper), Is.EqualTo(Just("FOO")));
    }

    [Test]
    public void FlatMap_NothingMaybeReturnedByFuncWithJustMaybe_ReturnsReturnedNothingMaybe()
    {
        Maybe<string> option = Just("Foo");
        Func<string, Maybe<string>> toNothing = _ => Nothing();

        Assert.That(option.FlatMap(toNothing), Is.EqualTo(Nothing()));
    }

    [Test]
    public void FlatMap_NothingMaybe_ReturnsNothingMaybe()
    {
        Maybe<string> option = Nothing();
        Func<string, Maybe<string>> toUpper = str => Just(str.ToUpper());

        Assert.That(option.FlatMap(toUpper), Is.EqualTo(Nothing()));
    }

    [Test]
    public void LinqQuery_WithMaybeType_ShouldFilterAndTransformCorrectly()
    {
        var maybe = Just("Foo");
        var just = from x in maybe where x.Length > 2 select x.ToUpper();
        var nothing = from x in maybe where x.Length < 2 select x.ToUpper();

        Assert.That(just.Value, Is.EqualTo("FOO"));
        Assert.That(nothing.IsNothing, Is.True);
    }
}
