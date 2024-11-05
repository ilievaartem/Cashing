using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cashing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LikeController : ControllerBase
{
    private readonly ILikeService _likeService;

    public LikeController(ILikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpGet("{dogOwnerId}/count")]
    public async Task<IActionResult> GetLikesCount(int dogOwnerId)
    {
        var count = await _likeService.GetLikesCountAsync(dogOwnerId);
        return Ok(count);
    }
}