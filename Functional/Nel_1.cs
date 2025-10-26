using System.Collections;

namespace Macaron.Functional;

public sealed partial class Nel<T> : IEnumerable<T>
{
    #region Fields
    private readonly T _head;

    private readonly Seq<T> _tail;
    #endregion

    #region Properties
    public T Head => _head;

    public Seq<T> Tail => _tail;
    #endregion

    #region Constructors
    public Nel(T head)
    {
        _head = head;
        _tail = Seq<T>.Nil;
    }

    public Nel(T head, Seq<T> tail)
    {
        _head = head;
        _tail = tail ?? throw new ArgumentNullException(nameof(tail));
    }

    public Nel(T head, Nel<T> tail)
    {
        _head = head;
        _tail = (tail ?? throw new ArgumentNullException(nameof(tail))).ToSeq();
    }
    #endregion

    #region IEnumerable<T> Interface
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion

    #region Methods
    public Enumerator GetEnumerator() => new(nel: this);

    public Nel<T> Prepend(T value) => new(value, new Seq<T>.Node(_head, _tail));

    public Nel<T> Append(Seq<T> other)
    {
        return other != null
            ? new Nel<T>(_head, _tail.Append(other))
            : throw new ArgumentNullException(nameof(other));
    }

    public Nel<T> Append(Nel<T> other)
    {
        return other != null
            ? new Nel<T>(_head, _tail.Append(other.ToSeq()))
            : throw new ArgumentNullException(nameof(other));
    }

    public Nel<T> Reverse()
    {
        var result = new Seq<T>.Node(_head, tail: Seq<T>.Nil);

        for (var seq = _tail; seq is Seq<T>.Node node; seq = node.Tail)
        {
            result = new Seq<T>.Node(node.Head, result);
        }

        return new Nel<T>(result.Head, result.Tail);
    }

    public void Deconstruct(out T head, out Seq<T> tail)
    {
        head = _head;
        tail = _tail;
    }
    #endregion
}
