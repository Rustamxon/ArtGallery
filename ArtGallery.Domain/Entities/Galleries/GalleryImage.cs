using ArtGallery.Domain.Commons;

namespace ArtGallery.Domain.Entities.Galleries;

public class GalleryImage : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
    public int GalleryId { get; set; }
    public Gallery Gallery { get; set; }
}
