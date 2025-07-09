namespace Macaron.Functional;

public readonly struct Maybe<T> : IEquatable<Maybe<T>>, IEquatable<JustMaybe<T>>, IEquatable<NothingMaybe>
{
    #region Implicit Conversion Operators
    public static implicit operator Maybe<T>(JustMaybe<T> just)
    {
        return new Maybe<T>(isJust: true, value: just.Value);
    }

    public static implicit operator Maybe<T>(NothingMaybe nothing)
    {
        return new Maybe<T>(isJust: false, value: default);
    }

    public static implicit operator Maybe<T>(NothingMaybe<T> nothing)
    {
        return new Maybe<T>(isJust: false, value: default);
    }
    #endregion

    #region Operator Overloadings
    public static bool operator ==(Maybe<T> left, Maybe<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Maybe<T> left, Maybe<T> right)
    {
        return !left.Equals(right);
    }

    public static bool operator ==(Maybe<T> left, JustMaybe<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Maybe<T> left, JustMaybe<T> right)
    {
        return !left.Equals(right);
    }

    public static bool operator ==(Maybe<T> left, NothingMaybe right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Maybe<T> left, NothingMaybe right)
    {
        return !left.Equals(right);
    }

    public static bool operator ==(Maybe<T> left, NothingMaybe<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Maybe<T> left, NothingMaybe<T> right)
    {
        return !left.Equals(right);
    }
    #endregion

    #region Fields
    private readonly bool? _isJust;
    private readonly T? _value;
    #endregion

    #region Properties
    public bool IsJust => _isJust ?? throw new InvalidOperationException("Maybe is not initialized.");

    public bool IsNothing => !IsJust;

    public T Value => IsJust ? _value! : throw new InvalidOperationException("Maybe is not Just.");
    #endregion

    #region Constructors
    private Maybe(bool isJust, T? value)
    {
        _isJust = isJust;
        _value = value;
    }
    #endregion

    #region Overrides
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Maybe<T> maybe => Equals(maybe),
            JustMaybe<T> just => Equals(just),
            NothingMaybe nothing => Equals(nothing),
            _ => false
        };
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_isJust, _value);
    }

    public override string ToString()
    {
        return IsJust ? $"Just({_value})" : "Nothing";
    }
    #endregion

    #region IEquatable<Maybe<T>> Interface
    public bool Equals(Maybe<T> other)
    {
        return _isJust == other._isJust && EqualityComparer<T?>.Default.Equals(_value, other._value);
    }
    #endregion

    #region IEquatable<JustMaybe<T>> Interface
    public bool Equals(JustMaybe<T> other)
    {
        return _isJust is true && EqualityComparer<T?>.Default.Equals(_value, other.Value);
    }
    #endregion

    #region IEquatable<NothingMaybe> Interface
    public bool Equals(NothingMaybe other)
    {
        return _isJust is false;
    }
    #endregion

    #region Methods
    public Maybe<TResult> Map<TResult>(Func<T, TResult> fn)
    {
        return IsJust switch
        {
            true => Maybe.Just(fn.Invoke(_value!)),
            false => Maybe.Nothing()
        };
    }

    public Maybe<TResult> FlatMap<TResult>(Func<T, Maybe<TResult>> fn)
    {
        return IsJust switch
        {
            true => fn.Invoke(_value!),
            false => Maybe.Nothing()
        };
    }

    public Maybe<T> Tap(Action<T> action)
    {
        if (IsJust)
        {
            action.Invoke(_value!);
        }

        return this;
    }

    public TResult Match<TResult>(Func<T, TResult> just, Func<TResult> nothing)
    {
        return IsJust ? just.Invoke(_value!) : nothing.Invoke();
    }

    public void Match(Action<T> just, Action nothing)
    {
        if (IsJust)
        {
            just.Invoke(_value!);
        }
        else
        {
            nothing.Invoke();
        }
    }
    #endregion
}
