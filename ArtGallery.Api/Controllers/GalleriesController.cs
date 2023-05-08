using ArtGallery.Domain.Configurations;
using ArtGallery.Service.DTOs.Galleries;
using ArtGallery.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtGallery.Api.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GalleriesController : ControllerBase
{
    private readonly IGalleryService galleryService;

    public GalleriesController(IGalleryService galleryService)
    {
        this.galleryService = galleryService;
    }

    [HttpGet("get-all")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async ValueTask<IActionResult> GetAllGaleriesAsync([FromQuery] PaginationParams @params)
        => Ok(await this.galleryService.RetrieveAllAsync(@params));

    [HttpGet("get-by-id")]
    public async ValueTask<IActionResult> GetByIdAsync(int id)
        => Ok(await this.galleryService.RetrieveByIdAsync(id));

    [HttpPost("add")]
    [AllowAnonymous]
    public async ValueTask<ActionResult> AddGalleryAsync(GalleryForCreationDto dto)
        => Ok(await this.galleryService.AddAsync(dto));

    [HttpPut("Update")]
    public async ValueTask<ActionResult> UpdateAsync(GalleryForUpdateDto dto)
        => Ok(await this.galleryService.ModifyOwnInfoAsync(dto));

    [HttpPut("update-as-admin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async ValueTask<ActionResult> UpdateAsAdmin(int id, GalleryForUpdateDto dto)
        => Ok(await this.galleryService.ModifyAsAdminAsync(id, dto));

    [HttpDelete("delete-by-id")]
    public async ValueTask<ActionResult<bool>> DeleteAsync(int id)
        => Ok(await this.galleryService.RemoveAsync(id));

    [HttpDelete("delete-as-admin")]
    [Authorize(Roles = "SuperAdmin")]
    public async ValueTask<ActionResult<bool>> DestroyAsync(int id)
        => Ok(await this.galleryService.DestroyAsync(id));

    [HttpPost("image-upload")]
    public async ValueTask<IActionResult> UploadImage([FromForm] GalleryImageForCreationDto dto)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.galleryService.ImageUploadAsync(dto)
        });

    [HttpDelete("image-delete/{galleryId:int}")]
    public async Task<IActionResult> DeleteUserImage(int galleryId)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.galleryService.DeleteGalleryImageAsync(galleryId)
        });

    [HttpGet("image-get/{galleryId:int}")]
    public async Task<IActionResult> GetUserImage(int userId)
        => Ok(new
        {
            Code = 200,
            Error = "Success",
            Data = await this.galleryService.GetGalleryImageAsync(userId)
        });
}
