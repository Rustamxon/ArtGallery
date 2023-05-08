using ArtGallery.Domain.Commons;
using ArtGallery.Domain.Enums;

namespace ArtGallery.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
}
