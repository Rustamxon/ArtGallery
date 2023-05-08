namespace ArtGallery.Domain.Commons;

public class Auditable
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public int? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}
