using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cashing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserProfile(int userId)
    {
        var user = await _userService.GetUserProfileAsync(userId);
        return user != null ? Ok(user) : NotFound();
    }
}
