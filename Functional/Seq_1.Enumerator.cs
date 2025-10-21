using System.Collections;

namespace Macaron.Functional;

partial class Seq<T>
{
    public struct Enumerator : IEnumerator<T>
    {
        #region Enums
        private enum State : byte
        {
            Before = 0,
            Valid = 1,
            After = 2,
        }
        #endregion

        #region Fields
        private readonly Seq<T> _initial;
        private Seq<T>? _current;
        private T _currentValue;
        private State _state;
        #endregion

        #region Constructors
        internal Enumerator(Seq<T> seq)
        {
            _initial = seq;
            _current = seq;
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
            if (_current == null)
            {
                _currentValue = default!;
                _state = State.After;

                return false;
            }
            else
            {
                var value = _current.Head;

                _current = _current.Tail;
                _currentValue = value;
                _state = State.Valid;

                return true;
            }
        }

        public void Reset()
        {
            _current = _initial;
            _currentValue = default!;
            _state = State.Before;
        }

        public void Dispose()
        {
        }
        #endregion
    }
}
