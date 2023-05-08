using ArtGallery.Domain.Enums;

namespace ArtGallery.Service.DTOs.Users;

public class UserForResultDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserRole UserRole { get; set; }
    public UserImageForResultDto Image { get; set; }
}
