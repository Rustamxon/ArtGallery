using ArtGallery.Domain.Configurations;
using ArtGallery.Service.DTOs.Galleries;

namespace ArtGallery.Service.Interfaces;

public interface IGalleryService
{
    Task<bool> RemoveAsync(int id);
    Task<bool> DestroyAsync(int id);
    Task<GalleryForResultDto> RetrieveByIdAsync(int id);
    Task<GalleryForResultDto> AddAsync(GalleryForCreationDto dto);
    Task<GalleryForResultDto> ModifyOwnInfoAsync(GalleryForUpdateDto dto);
    Task<GalleryForResultDto> ModifyAsAdminAsync(int id, GalleryForUpdateDto dto);
    Task<IEnumerable<GalleryForResultDto>> RetrieveAllAsync(PaginationParams @params);
    ValueTask<GalleryImageForResultDto> ImageUploadAsync(GalleryImageForCreationDto dto);
    ValueTask<GalleryImageForResultDto> GetGalleryImageAsync(int galleryId);
    ValueTask<bool> DeleteGalleryImageAsync(int galleryId);
}
