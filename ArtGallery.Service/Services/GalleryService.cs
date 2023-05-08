using ArtGallery.DAL.IRepositories;
using ArtGallery.Domain.Configurations;
using ArtGallery.Domain.Entities.Galleries;
using ArtGallery.Service.DTOs.Galleries;
using ArtGallery.Service.Exceptions;
using ArtGallery.Service.Extensions;
using ArtGallery.Service.Interfaces;
using ArtGallery.Shared.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Service.Services;

public class GalleryService : IGalleryService
{
    private readonly IMapper mapper;
    private readonly IRepository<Gallery> repository;
    private readonly IRepository<GalleryImage> galleryImageRepository;
    public GalleryService(
        IMapper mapper,
        IRepository<Gallery> repository,
        IRepository<GalleryImage> galleryImageRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.galleryImageRepository = galleryImageRepository;
    }
    public GalleryService() { }
    public async Task<GalleryForResultDto> AddAsync(GalleryForCreationDto dto)
    {
        var existGallery = await this.repository.SelectAsync(g => g.Name.ToLower().Equals(dto.Name.ToLower()) && g.OwnerId.Equals(HttpContextHelper.UserId));
        if (existGallery is not null && !existGallery.IsDeleted)
            throw new CustomException(404, "Gallery is already exist");

        var mapped = this.mapper.Map<Gallery>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        var addedModel = await this.repository.InsertAsync(mapped);

        await this.repository.SaveChangesAsync();

        return this.mapper.Map<GalleryForResultDto>(addedModel);
    }

    public async ValueTask<bool> DeleteGalleryImageAsync(int galleryId)
    {
        var galleryImage = await this.galleryImageRepository
            .SelectAsync(t => t.GalleryId.Equals(galleryId));
        if (galleryImage is null)
            throw new CustomException(404, "Image is not found");

        File.Delete(galleryImage.Path);
        await this.galleryImageRepository.DeleteAsync(galleryImage);
        await this.galleryImageRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DestroyAsync(int id)
    {
        var existGallery = await this.repository.SelectAsync(u => u.Id.Equals(id));
        if (existGallery is null || existGallery.IsDeleted)
            throw new CustomException(404, "Gallery not found");

        await this.repository.DeleteAsync(existGallery);
        await this.repository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<GalleryImageForResultDto> GetGalleryImageAsync(int galleryId)
    {
        var galleryImage = await this.galleryImageRepository.SelectAsync(t => t.GalleryId.Equals(galleryId));
        if (galleryImage is null)
            throw new CustomException(404, "Image is not found");
        return mapper.Map<GalleryImageForResultDto>(galleryImage);
    }

    public async ValueTask<GalleryImageForResultDto> ImageUploadAsync(GalleryImageForCreationDto dto)
    {
        var gallery = await this.repository.SelectAsync(t => t.Id.Equals(dto.GalleryId));
        if (gallery is null)
            throw new CustomException(404, "Gallery is not found");

        byte[] image = dto.Image.ToByteArray();
        var fileExtension = Path.GetExtension(dto.Image.FileName);
        var fileName = Guid.NewGuid().ToString("N") + fileExtension;
        var webRootPath = EnvironmentHelper.WebHostPath;
        var folder = Path.Combine(webRootPath, "uploads", "images", "Galleries");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var fullPath = Path.Combine(folder, fileName);
        using var imageStream = new MemoryStream(image);

        using var imagePath = new FileStream(fullPath, FileMode.CreateNew);
        imageStream.WriteTo(imagePath);

        var galleryImage = new GalleryImage
        {
            Name = fileName,
            Path = fullPath,
            GalleryId = dto.GalleryId,
            Gallery = gallery,
            CreatedAt = DateTime.UtcNow,
        };

        var createdImage = await this.galleryImageRepository.InsertAsync(galleryImage);
        await this.galleryImageRepository.SaveChangesAsync();
        return mapper.Map<GalleryImageForResultDto>(createdImage);
    }

    public async Task<GalleryForResultDto> ModifyAsAdminAsync(int id, GalleryForUpdateDto dto)
    {
        var existGallery = await this.repository.SelectAsync(u => u.Id.Equals(id));
        if (existGallery is null || existGallery.IsDeleted)
            throw new CustomException(404, "Gallery not found");
        if (existGallery.Name.ToLower().Equals(existGallery.Name.ToLower()))
            throw new CustomException(400, "This gallery is already registered");
        this.mapper.Map(dto, existGallery);
        existGallery.LastUpdatedAt = DateTime.UtcNow;
        existGallery.UpdatedBy = HttpContextHelper.UserId;
        await this.repository.SaveChangesAsync();

        return this.mapper.Map<GalleryForResultDto>(existGallery);
    }

    public async Task<GalleryForResultDto> ModifyOwnInfoAsync(GalleryForUpdateDto dto)
    {
        var galleryId = HttpContextHelper.UserId;
        var existGallery = await this.repository.SelectAsync(u => u.Id.Equals(galleryId));
        if (existGallery is null || existGallery.IsDeleted)
            throw new CustomException(404, "User not found");
        if (existGallery.Name.ToLower().Equals(dto.Name.ToLower()))
            throw new CustomException(400, "This Name is already registered");
        this.mapper.Map(dto, existGallery);
        existGallery.LastUpdatedAt = DateTime.UtcNow;
        existGallery.UpdatedBy = galleryId;
        await this.repository.SaveChangesAsync();

        return this.mapper.Map<GalleryForResultDto>(existGallery);
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var existGallery = await this.repository.SelectAsync(u => u.Id.Equals(id));
        if (existGallery is null || existGallery.IsDeleted)
            throw new CustomException(404, "Gallery not found");
        existGallery.IsDeleted = true;
        existGallery.DeletedBy = HttpContextHelper.UserId;
        await this.repository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<GalleryForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var galleries = await this.repository.SelectAll()
            .Where(u => u.IsDeleted == false)
            .ToPagedList(@params)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<GalleryForResultDto>>(galleries);
    }

    public async Task<GalleryForResultDto> RetrieveByIdAsync(int id)
    {
        var existGallery = await this.repository.SelectAsync(u => u.Id.Equals(id));
        if (existGallery is null || existGallery.IsDeleted)
            throw new CustomException(404, "User not found");

        return this.mapper.Map<GalleryForResultDto>(existGallery);
    }

}
