using Gametopia.Contracts.DTOs;
using Gametopia.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly ProfileService _profileService;

    public AuthController(UserManager<IdentityUser> userManager, IJwtService jwtService, ProfileService profileService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _profileService = profileService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        var user = new IdentityUser { UserName = model.Username, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        var profile = _profileService.UpdateProfileAsync(user.Id, string.Empty, user.UserName);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { Message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            return Unauthorized();

        var token = _jwtService.GenerateJwtTokenForUser(user);
        return Ok(new { Token = token });
    }
}