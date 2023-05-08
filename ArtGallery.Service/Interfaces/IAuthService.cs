using ArtGallery.Service.DTOs.Login;

namespace ArtGallery.Service.Interfaces;

public interface IAuthService
{
    Task<LoginForResultDto> AuthenticateAsync(string email, string password);
}
