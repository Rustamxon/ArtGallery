using ArtGallery.Domain.Entities.Galleries;
using ArtGallery.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.DAL.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<GalleryImage> GalleryImages { get; set; }
}
