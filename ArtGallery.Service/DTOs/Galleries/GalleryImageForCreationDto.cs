using Microsoft.AspNetCore.Http;

namespace ArtGallery.Service.DTOs.Galleries;

public class GalleryImageForCreationDto
{
    public IFormFile Image { get; set; }
    public int GalleryId { get; set; }
}
