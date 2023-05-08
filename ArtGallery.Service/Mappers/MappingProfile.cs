using ArtGallery.Domain.Entities.Galleries;
using ArtGallery.Domain.Entities.Users;
using ArtGallery.Service.DTOs.Galleries;
using ArtGallery.Service.DTOs.Users;
using AutoMapper;

namespace ArtGallery.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<UserForCreationDto, UserForUpdateDto>().ReverseMap();
        CreateMap<UserImage, UserImageForResultDto>().ReverseMap();
        CreateMap<UserImage, UserImageForCreationDto>().ReverseMap();


        // Gallery
        CreateMap<Gallery, GalleryForCreationDto>().ReverseMap();
        CreateMap<Gallery, GalleryForResultDto>().ReverseMap();
        CreateMap<Gallery, GalleryForUpdateDto>().ReverseMap();
        CreateMap<GalleryForCreationDto, GalleryForUpdateDto>().ReverseMap();
        CreateMap<GalleryImage, GalleryImageForResultDto>().ReverseMap();
        CreateMap<GalleryImage, GalleryImageForCreationDto>().ReverseMap();

    }
}
