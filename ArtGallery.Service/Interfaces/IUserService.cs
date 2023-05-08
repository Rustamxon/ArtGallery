using ArtGallery.Domain.Configurations;
using ArtGallery.Domain.Entities.Users;
using ArtGallery.Service.DTOs.Users;

namespace ArtGallery.Service.Interfaces;

public interface IUserService
{
    Task<bool> RemoveAsync(int id);
    Task<bool> DestroyAsync(int id);
    Task<User> RetrieveByEmailAsync(string email);
    Task<UserForResultDto> RetrieveByIdAsync(int id);
    Task<UserForResultDto> AddAsync(UserForCreationDto dto);
    Task<UserForResultDto> ModifyOwnInfoAsync(UserForUpdateDto dto);
    Task<UserForResultDto> ModifyAsAdminAsync(int id, UserForUpdateDto dto);
    Task<UserForResultDto> ChangePasswordAsync(UserForChangePasswordDto dto);
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<UserImageForResultDto> ImageUploadAsync(UserImageForCreationDto dto);
    Task<UserImageForResultDto> GetUserImageAsync(int userId);
    Task<bool> DeleteUserImageAsync(int userId);
}
