namespace App.Domain.Common;

public interface IHasOwner {
    Guid OwnerId { get; }
}
