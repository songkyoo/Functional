namespace Macaron.Functional.Tests;

[TestFixture]
public class SeqTests
{
    [Test]
    public void Constructor_SingleValue_CreatesSeqWithNullTail()
    {
        // Arrange & Act
        var seq = new Seq<int>(42);

        // Assert
        Assert.That(seq.Head, Is.EqualTo(42));
        Assert.That(seq.Tail, Is.Null);
    }

    [Test]
    public void Count_SingleElement_ReturnsOne()
    {
        // Arrange
        var seq = new Seq<string>("hello");

        // Act
        var count = seq.Count;

        // Assert
        Assert.That(count, Is.EqualTo(1));
    }

    [Test]
    public void Count_MultipleElements_ReturnsCorrectCount()
    {
        // Arrange
        var seq = Seq.Of(1, 2, 3, 4, 5);

        // Act
        var count = seq.Count;

        // Assert
        Assert.That(count, Is.EqualTo(5));
    }

    [Test]
    public void GetEnumerator_MultipleElements_EnumeratesInOrder()
    {
        // Arrange
        var seq = Seq.Of("a", "b", "c");
        var result = new List<string>();

        // Act
        foreach (var item in seq)
        {
            result.Add(item);
        }

        // Assert
        Assert.That(result, Is.EqualTo(new[] { "a", "b", "c" }).AsCollection);
    }

    [Test]
    public void Prepend_AddsElementAtBeginning()
    {
        // Arrange
        var seq = new Seq<int>(2);

        // Act
        var newSeq = seq.Prepend(1);

        // Assert
        Assert.That(newSeq.Head, Is.EqualTo(1));
        Assert.That(newSeq.Tail, Is.SameAs(seq));
        Assert.That(newSeq.Tail.Head, Is.EqualTo(2));
    }

    [Test]
    public void Append_SingleElementToSingle_CreatesCorrectSequence()
    {
        // Arrange
        var seq1 = new Seq<int>(1);
        var seq2 = new Seq<int>(2);

        // Act
        var result = seq1.Append(seq2);

        // Assert
        Assert.That(result.ToList(), Is.EqualTo(new[] { 1, 2 }).AsCollection);
    }

    [Test]
    public void Append_MultipleToMultiple_CreatesCorrectSequence()
    {
        // Arrange
        var seq1 = Seq.Of(1, 2, 3);
        var seq2 = Seq.Of(4, 5, 6);

        // Act
        var result = seq1.Append(seq2);

        // Assert
        Assert.That(result.ToList(), Is.EqualTo(new[] { 1, 2, 3, 4, 5, 6 }).AsCollection);
    }

    [Test]
    public void Deconstruct_ExtractsHeadAndTail()
    {
        // Arrange
        var seq = Seq.Of(10, 20, 30);

        // Act
        var (head, tail) = seq;

        // Assert
        Assert.That(head, Is.EqualTo(10));
        Assert.That(tail, Is.Not.Null);
        Assert.That(tail.Head, Is.EqualTo(20));
    }

    [Test]
    public void Of_SingleParameter_CreatesSingleElementSeq()
    {
        // Arrange & Act
        var seq = Seq.Of(100);

        // Assert
        Assert.That(seq.Head, Is.EqualTo(100));
        Assert.That(seq.Tail, Is.Null);
        Assert.That(seq, Has.Count.EqualTo(1));
    }

    [Test]
    public void Of_TwoParameters_CreatesCorrectSequence()
    {
        // Arrange & Act
        var seq = Seq.Of("first", "second");

        // Assert
        Assert.That(seq.ToList(), Is.EqualTo(new[] { "first", "second" }).AsCollection);
    }

    [Test]
    public void Of_EightParameters_CreatesCorrectSequence()
    {
        // Arrange & Act
        var seq = Seq.Of(1, 2, 3, 4, 5, 6, 7, 8);

        // Assert
        Assert.That(seq, Has.Count.EqualTo(8));
        Assert.That(seq.ToList(), Is.EqualTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8 }).AsCollection);
    }

    [Test]
    public void Of_WithParamsArray_CreatesCorrectSequence()
    {
        // Arrange & Act
        var seq = Seq.Of(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);

        // Assert
        Assert.That(seq.Count, Is.EqualTo(11));
        Assert.That(seq.ToList(), Is.EqualTo(Enumerable.Range(1, 11)).AsCollection);
    }

    [Test]
    public void Cons_SingleValue_PrependToSeq()
    {
        // Arrange
        var original = Seq.Of(2, 3);

        // Act
        var result = Seq.Cons(1, original);

        // Assert
        Assert.That(result.ToList(), Is.EqualTo(new[] { 1, 2, 3 }).AsCollection);
    }

    [Test]
    public void Cons_MultipleValues_PrependsInCorrectOrder()
    {
        // Arrange
        var original = Seq.Of(4, 5);

        // Act
        var result = Seq.Cons(1, 2, 3, original);

        // Assert
        Assert.That(result.ToList(), Is.EqualTo(new[] { 1, 2, 3, 4, 5 }).AsCollection);
    }

    [Test]
    public void Cons_EightValues_PrependsCorrectly()
    {
        // Arrange
        var original = Seq.Of(9);

        // Act
        var result = Seq.Cons(1, 2, 3, 4, 5, 6, 7, 8, original);

        // Assert
        Assert.That(result.ToList(), Is.EqualTo(Enumerable.Range(1, 9)).AsCollection);
    }

    [Test]
    public void Concat_TwoSequences_CombinesCorrectly()
    {
        // Arrange
        var first = Seq.Of("a", "b");
        var second = Seq.Of("c", "d");

        // Act
        var result = Seq.Concat(first, second);

        // Assert
        var items = result.ToList();

        Assert.That(items, Is.EqualTo(new[] { "a", "b", "c", "d" }).AsCollection);
    }

    [Test]
    public void ImmutabilityTest_PrependDoesNotModifyOriginal()
    {
        // Arrange
        var original = Seq.Of(2, 3);
        var originalCount = original.Count;

        // Act
        var modified = original.Prepend(1);

        // Assert
        Assert.That(original, Has.Count.EqualTo(originalCount));
        Assert.That(original.Head, Is.EqualTo(2));
        Assert.That(modified, Has.Count.EqualTo(originalCount + 1));
    }

    [Test]
    public void ComplexChaining_WorksCorrectly()
    {
        // Arrange
        var seq1 = Seq.Of(1, 2);
        var seq2 = Seq.Of(3, 4);

        // Act
        var result = seq1
            .Prepend(0)
            .Append(seq2)
            .Prepend(-1);

        // Assert
        var items = result.ToList();

        Assert.That(items, Is.EqualTo(new[] { -1, 0, 1, 2, 3, 4 }).AsCollection);
    }

    [Test]
    public void GenericTypes_WorkWithDifferentTypes()
    {
        // Arrange & Act
        var intSeq = Seq.Of(1, 2, 3);
        var stringSeq = Seq.Of("a", "b", "c");
        var doubleSeq = Seq.Of(1.1, 2.2, 3.3);

        // Assert
        Assert.That(intSeq, Has.Count.EqualTo(3));
        Assert.That(stringSeq, Has.Count.EqualTo(3));
        Assert.That(doubleSeq, Has.Count.EqualTo(3));
    }

    [Test]
    public void EmptyParamsArray_HandledCorrectly()
    {
        // Arrange & Act
        var seq = Seq.Of(1, 2, 3, 4, 5, 6, 7, 8, []);

        // Assert
        Assert.That(seq, Has.Count.EqualTo(8));
        Assert.That(seq.ToList(), Is.EqualTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8 }).AsCollection);
    }
}
