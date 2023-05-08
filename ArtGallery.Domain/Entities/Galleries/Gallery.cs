using ArtGallery.Domain.Commons;
using ArtGallery.Domain.Entities.Users;

namespace ArtGallery.Domain.Entities.Galleries;

public class Gallery : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }

    public int OwnerId { get; set; }
    public User User { get; set; }
}
