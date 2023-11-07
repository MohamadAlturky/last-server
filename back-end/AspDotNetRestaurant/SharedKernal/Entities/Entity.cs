using System.ComponentModel.DataAnnotations;

namespace SharedKernal.Entities;

public abstract class Entity : IEntity
{
	public long Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }

	// [Timestamp]
	// public byte[] RowVersion { get; set; }
	protected Entity(long id)
	{
		this.Id = id;
	}

	public bool Equals(Entity? other)
	{
		if (other is null) return false;

		if (this.GetType() != other.GetType()) return false;

		if (ReferenceEquals(this, other)) return true;

		return this.Id.Equals(other.Id);
	}

	public override bool Equals(object? obj)
	{
		if (obj == null)
		{
			return false;
		}
		return this.Equals(obj as Entity);
	}

	public static bool operator ==(Entity a, Entity b) => a.Equals(b);

	public static bool operator !=(Entity a, Entity b) => !a.Equals(b);

	public override int GetHashCode()
	{
		return Id.GetHashCode();
	}
}
