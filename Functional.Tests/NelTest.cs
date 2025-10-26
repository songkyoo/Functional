namespace Macaron.Functional.Tests;

[TestFixture]
public class NelTests
{
    [Test]
    public void Constructor_SingleValue_CreatesNelWithNilTail()
    {
        // Arrange & Act
        var nel = new Nel<int>(42);

        // Assert
        Assert.That(nel.Head, Is.EqualTo(42));
        Assert.That(nel.Tail, Is.EqualTo(Seq<int>.Nil));
        Assert.That(nel.Count(), Is.EqualTo(1));
    }

    [Test]
    public void Constructor_HeadAndSeqTail_WiresFields()
    {
        // Arrange
        var tail = Seq.Of(2, 3);

        // Act
        var nel = new Nel<int>(1, tail);

        // Assert
        Assert.That(nel.Head, Is.EqualTo(1));
        Assert.That(nel.Tail.ToList(), Is.EqualTo(new[] { 2, 3 }).AsCollection);
        Assert.That(nel.Count(), Is.EqualTo(3));
    }

    [Test]
    public void Constructor_HeadAndNelTail_ToSeqIsUsedInternally()
    {
        // Arrange
        var tailNel = Nel.Of(2, 3, 4);

        // Act
        var nel = new Nel<int>(1, tailNel);

        // Assert
        Assert.That(nel.Head, Is.EqualTo(1));
        Assert.That(nel.Tail.ToList(), Is.EqualTo(new[] { 2, 3, 4 }).AsCollection);
        Assert.That(nel.Count(), Is.EqualTo(4));
    }

    [Test]
    public void Count_SingleAndMultiple()
    {
        Assert.That(Nel.Of("a").Count(), Is.EqualTo(1));
        Assert.That(Nel.Of(1, 2, 3, 4).Count(), Is.EqualTo(4));
    }

    [Test]
    public void GetEnumerator_EnumeratesHeadThenTailInOrder()
    {
        // Arrange
        var nel = Nel.Of("a", "b", "c");

        // Act
        var items = new List<string>();
        foreach (var x in nel) items.Add(x);

        // Assert
        Assert.That(items, Is.EqualTo(new[] { "a", "b", "c" }).AsCollection);
    }

    [Test]
    public void Prepend_AddsNewHead_OriginalUnchanged()
    {
        // Arrange
        var original = Nel.Of(2, 3);
        var originalCount = original.Count();

        // Act
        var modified = original.Prepend(1);

        // Assert
        Assert.That(original.Count(), Is.EqualTo(originalCount));
        Assert.That(original.Head, Is.EqualTo(2));
        Assert.That(modified.Head, Is.EqualTo(1));
        Assert.That(modified.ToList(), Is.EqualTo(new[] { 1, 2, 3 }).AsCollection);
    }

    [Test]
    public void Append_WithSeq_AppendsToTail()
    {
        // Arrange
        var head = Nel.Of(1, 2);
        var other = Seq.Of(3, 4);

        // Act
        var result = head.Append(other);

        // Assert
        Assert.That(result.ToList(), Is.EqualTo(new[] { 1, 2, 3, 4 }).AsCollection);
    }

    [Test]
    public void Append_WithNel_AppendsToTail()
    {
        // Arrange
        var left = Nel.Of(1, 2);
        var right = Nel.Of(3, 4);

        // Act
        var result = left.Append(right);

        // Assert
        Assert.That(result.ToList(), Is.EqualTo(new[] { 1, 2, 3, 4 }).AsCollection);
    }

    [Test]
    public void Reverse_ReturnsReversedNel()
    {
        // Arrange
        var nel = Nel.Of(1, 2, 3, 4, 5);

        // Act
        var reversed = nel.Reverse();

        // Assert
        Assert.That(reversed.Count(), Is.EqualTo(5));
        Assert.That(reversed.ToList(), Is.EqualTo(new[] { 5, 4, 3, 2, 1 }).AsCollection);
    }

    [Test]
    public void Deconstruct_ExtractsHeadAndTail()
    {
        // Arrange
        var nel = Nel.Of(10, 20, 30);

        // Act
        var (head, tail) = nel;

        // Assert
        Assert.That(head, Is.EqualTo(10));
        Assert.That(tail, Is.Not.Null);
        Assert.That(tail.ToList(), Is.EqualTo(new[] { 20, 30 }).AsCollection);
    }

    [Test]
    public void Of_VariousArity_ConstructsCorrectly()
    {
        Assert.That(Nel.Of(100).ToList(), Is.EqualTo(new[] { 100 }).AsCollection);
        Assert.That(Nel.Of("first", "second").ToList(), Is.EqualTo(new[] { "first", "second" }).AsCollection);
        Assert.That(Nel.Of(1, 2, 3, 4, 5, 6, 7, 8).ToList(), Is.EqualTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8 }).AsCollection);
    }

    [Test]
    public void Of_WithParamsArray_EmptyTailArray_StillSingleNel()
    {
        // Arrange & Act
        var nel = Nel.Of(1, []);

        // Assert
        Assert.That(nel.Count(), Is.EqualTo(1));
        Assert.That(nel.ToList(), Is.EqualTo(new[] { 1 }).AsCollection);
    }

    [Test]
    public void Cons_WithSeqTail_WorksForMultipleHeads()
    {
        // Arrange
        var tail = Seq.Of(5, 6);

        // Act
        var r3 = Nel.Cons(1, 2, 3, tail);
        var r8 = Nel.Cons(1, 2, 3, 4, 5, 6, 7, 8, Seq<int>.Nil);

        // Assert
        Assert.That(r3.ToList(), Is.EqualTo(new[] { 1, 2, 3, 5, 6 }).AsCollection);
        Assert.That(r8.ToList(), Is.EqualTo(Enumerable.Range(1, 8)).AsCollection);
    }

    [Test]
    public void Cons_WithNelTail_WorksForMultipleHeads()
    {
        // Arrange
        var tail = Nel.Of(4, 5);

        // Act
        var r2 = Nel.Cons(1, 2, tail);
        var r4 = Nel.Cons(1, 2, 3, 4, tail);

        // Assert
        Assert.That(r2.ToList(), Is.EqualTo(new[] { 1, 2, 4, 5 }).AsCollection);
        Assert.That(r4.ToList(), Is.EqualTo(new[] { 1, 2, 3, 4, 4, 5 }).AsCollection);
    }

    [Test]
    public void Concat_NelWithSeq_And_NelWithNel()
    {
        // Arrange
        var n = Nel.Of('a', 'b');
        var s = Seq.Of('c', 'd');
        var m = Nel.Of('e', 'f');

        // Act
        var r1 = Nel.Concat(n, s);
        var r2 = Nel.Concat(n, m);

        // Assert
        Assert.That(r1.ToList(), Is.EqualTo(new[] { 'a', 'b', 'c', 'd' }).AsCollection);
        Assert.That(r2.ToList(), Is.EqualTo(new[] { 'a', 'b', 'e', 'f' }).AsCollection);
    }

    [Test]
    public void ToSeq_And_ToNel_RoundTrip()
    {
        // Arrange
        var nel = Nel.Of(1, 2, 3);

        // Act
        var seq = nel.ToSeq();
        var maybeNel = seq.ToNel();

        // Assert
        Assert.That(seq.ToList(), Is.EqualTo(new[] { 1, 2, 3 }).AsCollection);
        Assert.That(maybeNel.IsJust, Is.True);
        Assert.That(maybeNel.Value.ToList(), Is.EqualTo(new[] { 1, 2, 3 }).AsCollection);
    }

    [Test]
    public void ToNel_OnEmptySeq_ReturnsNothing()
    {
        // Arrange
        var empty = Seq<int>.Nil;

        // Act
        var maybeNel = empty.ToNel();

        // Assert
        Assert.That(maybeNel.IsNothing, Is.True);
    }
}

file static class NelTestHelpers
{
    public static List<T> ToList<T>(this Nel<T> nel)
    {
        return Enumerable.ToList(nel);
    }
}
