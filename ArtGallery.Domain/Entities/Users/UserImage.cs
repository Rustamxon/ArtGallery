using ArtGallery.Domain.Commons;

namespace ArtGallery.Domain.Entities.Users;

public class UserImage : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
