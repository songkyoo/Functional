using System.Collections;

namespace Macaron.Functional;

partial class Nel<T>
{
    public struct Enumerator : IEnumerator<T>
    {
        #region Enums
        private enum State : byte
        {
            Before,
            Valid,
            After,
        }
        #endregion

        #region Fields
        private readonly Nel<T> _initial;
        private Seq<T>? _current;
        private T _currentValue;
        private State _state;
        #endregion

        #region Constructors
        internal Enumerator(Nel<T> nel)
        {
            _initial = nel;
            _current = null;
            _currentValue = default!;
            _state = State.Before;
        }
        #endregion

        #region IEnumerator<T> Interface
        public T Current
        {
            get
            {
                return _state switch
                {
                    State.Valid => _currentValue,
                    State.Before => throw new InvalidOperationException("Enumeration has not started."),
                    State.After or _ => throw new InvalidOperationException("Enumeration already finished."),
                };
            }
        }

        object? IEnumerator.Current => Current;

        public bool MoveNext()
        {
            switch (_current)
            {
                case Seq<T>.Node node:
                {
                    _currentValue = node.Head;
                    _current = node.Tail;
                    _state = State.Valid;

                    return true;
                }
                case Seq<T>.Empty:
                {
                    _current = null;
                    _currentValue = default!;
                    _state = State.After;

                    return false;
                }
                case null when _state == State.Before:
                {
                    _currentValue = _initial.Head;
                    _current = _initial.Tail;
                    _state = State.Valid;

                    return true;
                }
                case null:
                {
                    _currentValue = default!;
                    _state = State.After;

                    return false;
                }
                default:
                    throw new InvalidOperationException("Unexpected sequence type.");
            }
        }

        public void Reset()
        {
            _current = null;
            _currentValue = default!;
            _state = State.Before;
        }

        public void Dispose()
        {
        }
        #endregion
    }
}
