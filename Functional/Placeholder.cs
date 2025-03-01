namespace Macaron.Functional;

public readonly struct Placeholder : IEquatable<Placeholder>
{
    #region Static
    public static readonly Placeholder _ = new();
    #endregion

    #region Inheritances
    public override bool Equals(object? obj) => obj is Placeholder;

    public override int GetHashCode() => 0;

    public override string ToString() => "_";
    #endregion

    #region IEquatable<Placeholder>
    public bool Equals(Placeholder other) => true;
    #endregion
}
