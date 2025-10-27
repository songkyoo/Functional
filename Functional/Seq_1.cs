using System.Buffers;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Macaron.Functional;

public abstract partial class Seq<T> : IEnumerable<T>
{
    #region Constants
    public static readonly Seq<T> Nil = new Empty();
    #endregion

    #region Types
    public sealed class Empty : Seq<T>
    {
        internal Empty()
        {
        }
    }

    public sealed class Node : Seq<T>
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
        public Node(T head, Seq<T> tail)
        {
            _head = head;
            _tail = tail ?? throw new ArgumentNullException(nameof(tail));
        }
        #endregion

        #region Methods
        public void Deconstruct(out T head, out Seq<T> tail)
        {
            head = _head;
            tail = _tail;
        }
        #endregion
    }
    #endregion

    #region Properties
    public bool IsEmpty => this is Empty;
    #endregion

    #region IEnumerable<T> Interface
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion

    #region Methods
    public Enumerator GetEnumerator() => new(seq: this);

    public Seq<T> Prepend(T value) => new Node(value, tail: this);

    public Seq<T> Concat(Seq<T> other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        if (IsEmpty)
        {
            return other;
        }

        if (other.IsEmpty)
        {
            return this;
        }

        var pool = ArrayPool<T>.Shared;
        var buffer = pool.Rent(8);
        var count = 0;
        var isClearRequired = RuntimeHelpers.IsReferenceOrContainsReferences<T>();

        try
        {
            for (var seq = this; seq is Node node; seq = node.Tail)
            {
                if (count == buffer.Length)
                {
                    var newBuffer = pool.Rent(buffer.Length * 2);

                    Array.Copy(
                        sourceArray: buffer,
                        sourceIndex: 0,
                        destinationArray: newBuffer,
                        destinationIndex: 0,
                        length: count
                    );
                    pool.Return(buffer, clearArray: isClearRequired);

                    buffer = newBuffer;
                }

                buffer[count] = node.Head;
                count += 1;
            }

            var result = other;

            for (var i = count - 1; i >= 0; i--)
            {
                result = new Node(buffer[i], result);
            }

            return result;
        }
        finally
        {
            pool.Return(buffer, clearArray: isClearRequired);
        }
    }

    public Seq<T> Reverse()
    {
        var result = Nil;

        for (var seq = this; seq is Node node; seq = node.Tail)
        {
            result = new Node(node.Head, result);
        }

        return result;
    }
    #endregion
}
