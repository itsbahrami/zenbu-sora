namespace App.Domain.Common;

public interface ISoftDeletable {
    DateTimeOffset? DeletedAt { get; protected set; }

    public bool IsDeleted => DeletedAt != null;

    public void Recover() => DeletedAt = null;
    public void Delete() => DeletedAt = DateTimeOffset.UtcNow;
}
