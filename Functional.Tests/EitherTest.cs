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
        Either<string, string> either = Right("Foo");

        Assert.That(either.IsLeft, Is.False);
        Assert.That(either.IsRight, Is.True);
        Assert.That(() => either.Left, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(either.Right, Is.EqualTo("Foo"));
    }

    [Test]
    public void Right_WithTypeAndRight_ReturnsRightEither()
    {
        Either<string, string> either = Right<string, string>("Foo");

        Assert.That(either.IsLeft, Is.False);
        Assert.That(either.IsRight, Is.True);
        Assert.That(() => either.Left, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(either.Right, Is.EqualTo("Foo"));
    }

    [Test]
    public void Left_WithLeft_ReturnsLeftEither()
    {
        Either<string, string> either = Left("Bar");

        Assert.That(either.IsLeft, Is.True);
        Assert.That(either.IsRight, Is.False);
        Assert.That(() => either.Right, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(either.Left, Is.EqualTo("Bar"));
    }

    [Test]
    public void Left_WithTypeAndRight_ReturnsLeftEither()
    {
        Either<string, string> either = Left<string, string>("Foo");

        Assert.That(either.IsLeft, Is.True);
        Assert.That(either.IsRight, Is.False);
        Assert.That(() => either.Right, Throws.Exception.InstanceOf<InvalidOperationException>());
        Assert.That(either.Left, Is.EqualTo("Foo"));
    }

    [Test]
    public void GetOrElse_RightEither_ReturnsEitherRight()
    {
        Either<string, string> either = Right("Foo");

        Assert.That(either.GetOrElse("Bar"), Is.EqualTo("Foo"));
    }

    [Test]
    public void GetOrElse_LeftEither_ReturnsReplacementRight()
    {
        Either<string, string> either = Left("Foo");

        Assert.That(either.GetOrElse("Bar"), Is.EqualTo("Bar"));
    }

    [Test]
    public void OrElse_RightEither_ReturnsSelf()
    {
        Either<string, string> either = Right("Foo");

        Assert.That(either.OrElse("Bar"), Is.EqualTo(Right("Foo")));
    }

    [Test]
    public void OrElse_LeftEither_ReturnsRightEither()
    {
        Either<string, string> either = Left("Foo");

        Assert.That(either.OrElse("Bar"), Is.EqualTo(Right("Bar")));
    }

    [Test]
    public void Map_RightEither_ReturnsRightEither()
    {
        Either<string, string> either = Right("Foo");
        Func<string, string> toUpper = str => str.ToUpper();

        Assert.That(either.Map(toUpper), Is.EqualTo(Right("FOO")));
    }

    [Test]
    public void Map_LeftEither_ReturnsLeftEither()
    {
        Either<string, string> either = Left("Foo");
        Func<string, string> toUpper = str => str.ToUpper();

        Assert.That(either.Map(toUpper), Is.EqualTo(Left("Foo")));
    }

    [Test]
    public void FlatMap_RightEitherReturnedByFuncWithRightEither_ReturnsReturnedRightEither()
    {
        Either<string, string> either = Right("Foo");
        Func<string, Either<string, string>> toUpper = str => Right(str.ToUpper());

        Assert.That(either.FlatMap(toUpper), Is.EqualTo(Right("FOO")));
    }

    [Test]
    public void FlatMap_LeftEitherReturnedByFuncWithRightEither_ReturnsReturnedLeftEither()
    {
        Either<string, string> either = Right("Foo");
        Func<string, Either<string, string>> toUpper = str => Left(str.ToUpper());

        Assert.That(either.FlatMap(toUpper), Is.EqualTo(Left("FOO")));
    }

    [Test]
    public void FlatMap_RightEitherReturnedByFuncWithLeftEither_ReturnsLeftEither()
    {
        Either<string, string> either = Left("Foo");
        Func<string, Either<string, string>> toUpper = str => Right(str.ToUpper());

        Assert.That(either.FlatMap(toUpper), Is.EqualTo(Left("Foo")));
    }

    [Test]
    public void FlatMap_LeftEitherReturnedByFuncWithLeftEither_ReturnsLeftEither()
    {
        Either<string, string> either = Left("Foo");
        Func<string, Either<string, string>> toUpper = str => Left(str.ToUpper());

        Assert.That(either.FlatMap(toUpper), Is.EqualTo(Left("Foo")));
    }

    [Test]
    public void MapLeft_RightEither_ReturnsRightEither()
    {
        Either<string, string> either = Right("Foo");
        Func<string, string> toUpper = str => str.ToUpper();

        Assert.That(either.MapLeft(toUpper), Is.EqualTo(Right("Foo")));
    }

    [Test]
    public void MapLeft_LeftEither_ReturnsLeftEither()
    {
        Either<string, string> either = Left("Foo");
        Func<string, string> toUpper = str => str.ToUpper();

        Assert.That(either.MapLeft(toUpper), Is.EqualTo(Left("FOO")));
    }

    [Test]
    public void FlatMapLeft_RightEitherReturnedByFuncWithRightEither_ReturnsRightEither()
    {
        Either<string, string> either = Right("Foo");
        Func<string, Either<string, string>> toUpper = str => Right(str.ToUpper());

        Assert.That(either.FlatMapLeft(toUpper), Is.EqualTo(Right("Foo")));
    }

    [Test]
    public void FlatMapLeft_LeftEitherReturnedByFuncWithRightEither_ReturnsRightEither()
    {
        Either<string, string> either = Right("Foo");
        Func<string, Either<string, string>> toUpper = str => Left(str.ToUpper());

        Assert.That(either.FlatMapLeft(toUpper), Is.EqualTo(Right("Foo")));
    }

    [Test]
    public void FlatMapLeft_LeftEitherReturnedByFuncWithLeftEither_ReturnsReturnedLeftEither()
    {
        Either<string, string> either = Left("Foo");
        Func<string, Either<string, string>> toUpper = str => Left(str.ToUpper());

        Assert.That(either.FlatMapLeft(toUpper), Is.EqualTo(Left("FOO")));
    }

    [Test]
    public void FlatMapLeft_RightEitherReturnedByFuncWithLeftEither_ReturnsReturnedRightEither()
    {
        Either<string, string> either = Left("Foo");
        Func<string, Either<string, string>> toUpper = str => Right(str.ToUpper());

        Assert.That(either.FlatMapLeft(toUpper), Is.EqualTo(Right("FOO")));
    }
}
