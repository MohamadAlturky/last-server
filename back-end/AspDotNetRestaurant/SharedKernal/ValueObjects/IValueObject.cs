namespace SharedKernal.ValueObjects;

public interface IValueObject<T> where T : IEquatable<T>
{
	T Value { get; }
}
