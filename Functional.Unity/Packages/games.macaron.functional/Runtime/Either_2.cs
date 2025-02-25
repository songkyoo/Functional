using System;
using System.Collections.Generic;

namespace Macaron.Functional;

public readonly struct Either<TLeft, TRight>
    : IEquatable<Either<TLeft, TRight>>
    , IEquatable<LeftEither<TLeft>>
    , IEquatable<LeftEither<TLeft, TRight>>
    , IEquatable<RightEither<TRight>>
    , IEquatable<RightEither<TLeft, TRight>>
{
    #region Implicit Conversion Operators
    public static implicit operator Either<TLeft, TRight>(in LeftEither<TLeft> left)
    {
        return new Either<TLeft, TRight>(isRight: false, right: default, left: left.Value);
    }

    public static implicit operator Either<TLeft, TRight>(in LeftEither<TLeft, TRight> left)
    {
        return new Either<TLeft, TRight>(isRight: false, right: default, left: left.Value);
    }

    public static implicit operator Either<TLeft, TRight>(in RightEither<TRight> right)
    {
        return new Either<TLeft, TRight>(isRight: true, right: right.Value, left: default);
    }

    public static implicit operator Either<TLeft, TRight>(in RightEither<TLeft, TRight> right)
    {
        return new Either<TLeft, TRight>(isRight: true, right: right.Value, left: default);
    }
    #endregion

    #region Fields
    private readonly bool? _isRight;
    private readonly TLeft? _left;
    private readonly TRight? _right;
    #endregion

    #region Properties
    public bool IsLeft => !IsRight;

    public bool IsRight => _isRight ?? throw new InvalidOperationException("Either is not initialized.");

    public TLeft Left => IsLeft ? _left! : throw new InvalidOperationException("Either is not Left.");

    public TRight Right => IsRight ? _right! : throw new InvalidOperationException("Either is not Right.");
    #endregion

    #region Constructors
    private Either(bool isRight, in TLeft? left, in TRight? right)
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
            _ => false
        };
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_isRight, _left, _right);
    }

    public override string ToString()
    {
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
    public TRight GetOrElse(in TRight value)
    {
        return IsRight ? _right! : value;
    }

    public Either<TLeft, TRight> OrElse(in TRight value)
    {
        return IsRight switch
        {
            true => this,
            false => Either.Right(value)
        };
    }

    public Either<TLeft, TResult> Map<TResult>(Func<TRight, TResult> fn)
    {
        return IsRight switch
        {
            true => Either.Right(fn(_right!)),
            false => Either.Left<TLeft, TResult>(_left!)
        };
    }

    public Either<TLeft, TResult> FlatMap<TResult>(Func<TRight, Either<TLeft, TResult>> fn)
    {
        return IsRight switch
        {
            true => fn(_right!),
            false => Either.Left<TLeft, TResult>(_left!)
        };
    }

    public Either<TResult, TRight> MapLeft<TResult>(Func<TLeft, TResult> fn)
    {
        return IsRight switch
        {
            true => Either.Right<TResult, TRight>(_right!),
            false => Either.Left(fn(_left!))
        };
    }

    public Either<TResult, TRight> FlatMapLeft<TResult>(Func<TLeft, Either<TResult, TRight>> fn)
    {
        return IsRight switch
        {
            true => Either.Right<TResult, TRight>(_right!),
            false => fn(_left!)
        };
    }
    #endregion
}
