using System.Diagnostics.CodeAnalysis;

namespace Macaron.Functional;

public readonly struct Either<TLeft, TRight>
    : IEquatable<Either<TLeft, TRight>>
    , IEquatable<LeftEither<TLeft>>
    , IEquatable<LeftEither<TLeft, TRight>>
    , IEquatable<RightEither<TRight>>
    , IEquatable<RightEither<TLeft, TRight>>
{
    #region Implicit Conversion Operators
    public static implicit operator Either<TLeft, TRight>(LeftEither<TLeft> left)
    {
        return new Either<TLeft, TRight>(isRight: false, right: default, left: left.Value);
    }

    public static implicit operator Either<TLeft, TRight>(LeftEither<TLeft, TRight> left)
    {
        return new Either<TLeft, TRight>(isRight: false, right: default, left: left.Value);
    }

    public static implicit operator Either<TLeft, TRight>(RightEither<TRight> right)
    {
        return new Either<TLeft, TRight>(isRight: true, right: right.Value, left: default);
    }

    public static implicit operator Either<TLeft, TRight>(RightEither<TLeft, TRight> right)
    {
        return new Either<TLeft, TRight>(isRight: true, right: right.Value, left: default);
    }
    #endregion

    #region Operator Overloadings
    public static bool operator ==(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
    {
        return !(left == right);
    }

    public static bool operator ==(Either<TLeft, TRight> left, RightEither<TRight> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Either<TLeft, TRight> left, RightEither<TRight> right)
    {
        return !(left == right);
    }

    public static bool operator ==(Either<TLeft, TRight> left, RightEither<TLeft, TRight> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Either<TLeft, TRight> left, RightEither<TLeft, TRight> right)
    {
        return !(left == right);
    }

    public static bool operator ==(Either<TLeft, TRight> left, LeftEither<TLeft> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Either<TLeft, TRight> left, LeftEither<TLeft> right)
    {
        return !(left == right);
    }

    public static bool operator ==(Either<TLeft, TRight> left, LeftEither<TLeft, TRight> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Either<TLeft, TRight> left, LeftEither<TLeft, TRight> right)
    {
        return !(left == right);
    }
    #endregion

    #region Fields
    private readonly bool? _isRight;
    private readonly TLeft? _left;
    private readonly TRight? _right;
    #endregion

    #region Properties
    [MemberNotNullWhen(true, nameof(_left))]
    [MemberNotNullWhen(false, nameof(_right))]
    public bool IsLeft => !IsRight;

    [MemberNotNullWhen(true, nameof(_right))]
    [MemberNotNullWhen(false, nameof(_left))]
    public bool IsRight => _isRight ?? throw new InvalidOperationException("Either is not initialized.");

    public TLeft Left => IsLeft ? _left : throw new InvalidOperationException("Either is not Left.");

    public TRight Right => IsRight ? _right : throw new InvalidOperationException("Either is not Right.");
    #endregion

    #region Constructors
    private Either(bool isRight, TLeft? left, TRight? right)
    {
        _isRight = isRight;
        _left = left;
        _right = right;
    }
    #endregion

    #region Overrides
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Either<TLeft, TRight> either => Equals(either),
            LeftEither<TLeft> left => Equals(left),
            LeftEither<TLeft, TRight> left => Equals(left),
            RightEither<TRight> right => Equals(right),
            RightEither<TLeft, TRight> right => Equals(right),
            _ => false,
        };
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_isRight, _left, _right);
    }

    public override string ToString()
    {
        if (_isRight is null)
        {
            return "Either";
        }

        return IsRight ? $"Right({_right})" : $"Left({_left})";
    }
    #endregion

    #region IEquatable<Either<TLeft, TRight>> Interface
    public bool Equals(Either<TLeft, TRight> other)
    {
        return _isRight == other._isRight
            && EqualityComparer<TRight?>.Default.Equals(_right, other._right)
            && EqualityComparer<TLeft?>.Default.Equals(_left, other._left);
    }
    #endregion

    #region IEquatable<LeftEither<TLeft>> Interface
    public bool Equals(LeftEither<TLeft> other)
    {
        return _isRight is false && EqualityComparer<TLeft?>.Default.Equals(_left, other.Value);
    }
    #endregion

    #region IEquatable<LeftEither<TLeft, TRight>> Interface
    public bool Equals(LeftEither<TLeft, TRight> other)
    {
        return _isRight is false && EqualityComparer<TLeft?>.Default.Equals(_left, other.Value);
    }
    #endregion

    #region IEquatable<RightEither<TRight>> Interface
    public bool Equals(RightEither<TRight> other)
    {
        return _isRight is true && EqualityComparer<TRight?>.Default.Equals(_right, other.Value);
    }
    #endregion

    #region IEquatable<RightEither<TLeft, TRight>> Interface
    public bool Equals(RightEither<TLeft, TRight> other)
    {
        return _isRight is true && EqualityComparer<TRight?>.Default.Equals(_right, other.Value);
    }
    #endregion

    #region Methods
    public Either<TLeft, TResult> Map<TResult>(Func<TRight, TResult> fn)
    {
        return IsRight ? Either.Right<TLeft, TResult>(fn(_right)) : Either.Left<TLeft, TResult>(_left);
    }

    public Either<TResult, TRight> MapLeft<TResult>(Func<TLeft, TResult> fn)
    {
        return IsLeft switch
        {
            true => Either.Left<TResult, TRight>(fn(_left)),
            false => Either.Right<TResult, TRight>(_right),
        };
    }

    public Either<TLeft, TResult> FlatMap<TResult>(Func<TRight, Either<TLeft, TResult>> fn)
    {
        return IsRight switch
        {
            true => fn(_right),
            false => Either.Left<TLeft, TResult>(_left),
        };
    }

    public Either<TResult, TRight> FlatMapLeft<TResult>(Func<TLeft, Either<TResult, TRight>> fn)
    {
        return IsLeft switch
        {
            true => fn(_left),
            false => Either.Right<TResult, TRight>(_right),
        };
    }

    public Either<TLeft, TRight> Tap(Action<TRight> action)
    {
        if (IsRight)
        {
            action(_right);
        }

        return this;
    }

    public Either<TLeft, TRight> TapLeft(Action<TLeft> action)
    {
        if (IsLeft)
        {
            action(_left);
        }

        return this;
    }

    public void Match(Action<TLeft> left, Action<TRight> right)
    {
        if (IsRight)
        {
            right(_right);
        }
        else
        {
            left(_left);
        }
    }

    public TResult Match<TResult>(Func<TLeft, TResult> left, Func<TRight, TResult> right)
    {
        return IsRight ? right(_right) : left(_left);
    }
    #endregion
}
