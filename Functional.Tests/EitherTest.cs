using static Macaron.Functional.Either;

namespace Macaron.Functional.Tests;

[TestFixture]
public class EitherTest
{
    [Test]
    public void DefaultCtor_WithoutRightOrLeft_CreateInvalidEitherObject()
    {
        Either<string, string> either = default;

        Assert.That(() => either.IsLeft, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(() => either.IsRight, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(() => either.Left, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(() => either.Right, Throws.Exception.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void Right_WithRight_ReturnsRightEither()
    {
        Either<string, string> right = Right("Foo");

        Assert.That(right.IsLeft, Is.False);
        Assert.That(right.IsRight, Is.True);
        Assert.That(() => right.Left, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(right.Right, Is.EqualTo("Foo"));
    }

    [Test]
    public void Right_WithTypeAndRight_ReturnsRightEither()
    {
        Either<string, string> right = Right<string, string>("Foo");

        Assert.That(right.IsLeft, Is.False);
        Assert.That(right.IsRight, Is.True);
        Assert.That(() => right.Left, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(right.Right, Is.EqualTo("Foo"));
    }

    [Test]
    public void Left_WithLeft_ReturnsLeftEither()
    {
        Either<string, string> left = Left("Bar");

        Assert.That(left.IsLeft, Is.True);
        Assert.That(left.IsRight, Is.False);
        Assert.That(() => left.Right, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(left.Left, Is.EqualTo("Bar"));
    }

    [Test]
    public void Left_WithTypeAndLeft_ReturnsLeftEither()
    {
        Either<string, string> left = Left<string, string>("Foo");

        Assert.That(left.IsLeft, Is.True);
        Assert.That(left.IsRight, Is.False);
        Assert.That(() => left.Right, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(left.Left, Is.EqualTo("Foo"));
    }

    [Test]
    public void GetOrElse_RightEither_ReturnsEitherRight()
    {
        Either<string, string> right = Right("Foo");

        Assert.That(right.GetOrElse("Bar"), Is.EqualTo("Foo"));
    }

    [Test]
    public void Map_RightEither_ReturnsRightEither()
    {
        Either<string, string> right = Right("Foo");
        Func<string, string> toUpper = str => str.ToUpper();

        Assert.That(right.Map(toUpper), Is.EqualTo(Right("FOO")));
    }

    [Test]
    public void Map_LeftEither_ReturnsLeftEither()
    {
        Either<string, string> left = Left("Foo");
        Func<string, string> toUpper = str => str.ToUpper();

        Assert.That(left.Map(toUpper), Is.EqualTo(Left("Foo")));
    }

    [Test]
    public void MapLeft_RightEither_ReturnsRightEither()
    {
        Either<string, string> right = Right("Foo");
        Func<string, string> toUpper = str => str.ToUpper();

        Assert.That(right.MapLeft(toUpper), Is.EqualTo(Right("Foo")));
    }

    [Test]
    public void MapLeft_LeftEither_ReturnsLeftEither()
    {
        Either<string, string> left = Left("Foo");
        Func<string, string> toUpper = str => str.ToUpper();

        Assert.That(left.MapLeft(toUpper), Is.EqualTo(Left("FOO")));
    }

    [Test]
    public void FlatMap_RightEitherReturnedByFuncWithRightEither_ReturnsReturnedRightEither()
    {
        Either<string, string> right = Right("Foo");
        Func<string, Either<string, string>> toUpper = str => Right(str.ToUpper());

        Assert.That(right.FlatMap(toUpper), Is.EqualTo(Right("FOO")));
    }

    [Test]
    public void FlatMap_LeftEitherReturnedByFuncWithRightEither_ReturnsReturnedLeftEither()
    {
        Either<string, string> right = Right("Foo");
        Func<string, Either<string, string>> toUpper = str => Left(str.ToUpper());

        Assert.That(right.FlatMap(toUpper), Is.EqualTo(Left("FOO")));
    }

    [Test]
    public void FlatMap_RightEitherReturnedByFuncWithLeftEither_ReturnsLeftEither()
    {
        Either<string, string> left = Left("Foo");
        Func<string, Either<string, string>> toUpper = str => Right(str.ToUpper());

        Assert.That(left.FlatMap(toUpper), Is.EqualTo(Left("Foo")));
    }

    [Test]
    public void FlatMap_LeftEitherReturnedByFuncWithLeftEither_ReturnsLeftEither()
    {
        Either<string, string> left = Left("Foo");
        Func<string, Either<string, string>> toUpper = str => Left(str.ToUpper());

        Assert.That(left.FlatMap(toUpper), Is.EqualTo(Left("Foo")));
    }

    [Test]
    public void FlatMapLeft_RightEitherReturnedByFuncWithRightEither_ReturnsRightEither()
    {
        Either<string, string> right = Right("Foo");
        Func<string, Either<string, string>> toUpper = str => Right(str.ToUpper());

        Assert.That(right.FlatMapLeft(toUpper), Is.EqualTo(Right("Foo")));
    }

    [Test]
    public void FlatMapLeft_LeftEitherReturnedByFuncWithRightEither_ReturnsRightEither()
    {
        Either<string, string> right = Right("Foo");
        Func<string, Either<string, string>> toUpper = str => Left(str.ToUpper());

        Assert.That(right.FlatMapLeft(toUpper), Is.EqualTo(Right("Foo")));
    }

    [Test]
    public void FlatMapLeft_LeftEitherReturnedByFuncWithLeftEither_ReturnsReturnedLeftEither()
    {
        Either<string, string> left = Left("Foo");
        Func<string, Either<string, string>> toUpper = str => Left(str.ToUpper());

        Assert.That(left.FlatMapLeft(toUpper), Is.EqualTo(Left("FOO")));
    }

    [Test]
    public void FlatMapLeft_RightEitherReturnedByFuncWithLeftEither_ReturnsReturnedRightEither()
    {
        Either<string, string> left = Left("Foo");
        Func<string, Either<string, string>> toUpper = str => Right(str.ToUpper());

        Assert.That(left.FlatMapLeft(toUpper), Is.EqualTo(Right("FOO")));
    }

    [Test]
    public void Match_LeftEither_ReturnsLeftValue()
    {
        Either<string, string> left = Left("Foo");

        Assert.That(
            actual: left.Match(
                left: value => value,
                right: _ => "Bar"
            ),
            expression: Is.EqualTo("Foo")
        );
    }

    [Test]
    public void Match_RightEither_ReturnsRightValue()
    {
        Either<string, string> right = Right("Foo");

        Assert.That(
            actual: right.Match(
                left: _ => "Bar",
                right: value => value
            ),
            expression: Is.EqualTo("Foo")
        );
    }

    [Test]
    public void Match_LeftEither_ExecutesLeftAction()
    {
        Either<string, string> left = Left("Foo");

        var executed = false;
        left.Match(left: _ => executed = true, right: _ => { });

        Assert.That(executed, Is.True);
    }

    [Test]
    public void Match_RightEither_ExecutesRightAction()
    {
        Either<string, string> right = Right("Foo");

        var executed = false;
        right.Match(left: _ => { }, right: _ => executed = true);

        Assert.That(executed, Is.True);
    }
}
