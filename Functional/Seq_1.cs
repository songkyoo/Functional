using System.Collections;

namespace Macaron.Functional;

public sealed class Seq<T> : IReadOnlyCollection<T>
{
    #region Fields
    readonly T _head;
    readonly Seq<T>? _tail;
    #endregion

    #region Properties
    public T Head => _head;

    public Seq<T>? Tail => _tail;
    #endregion

    #region Constructors
    public Seq(T value)
    {
        _head = value;
        _tail = null;
    }

    internal Seq(T head, Seq<T>? tail)
    {
        _head = head;
        _tail = tail;
    }
    #endregion

    #region IReadOnlyCollection<T> Interface
    public int Count
    {
        get
        {
            var count = 0;
            var seq = this;

            while (seq != null)
            {
                count++;
                seq = seq.Tail;
            }

            return count;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var seq = this;

        while (seq != null)
        {
            yield return seq.Head;

            seq = seq.Tail;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion

    #region Methods
    public Seq<T> Prepend(T value)
    {
        return new Seq<T>(value, this);
    }

    public Seq<T> Append(Seq<T> other)
    {
        return _tail == null ? other.Prepend(_head) : _tail.Append(other).Prepend(_head);
    }
    #endregion
}
