using Gametopia.Contracts.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly ProfileService _profileService;

    public ProfileController(ProfileService userService)
    {
        _profileService = userService;
    }

    // Obtener datos del usuario actual
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _profileService.GetProfileByIdAsync(userId);

        if (user == null)
        {
            return NotFound("Usuario no encontrado.");
        }

        return Ok(new
        {
            user.Id,
            user.SteamUser,
            user.Nick
        });
    }

    // Actualizar los datos del usuario
    [HttpPut("me")]
    public async Task<IActionResult> UpdateCurrentUser([FromBody] ProfileUpdateDto model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var updated = await _profileService.UpdateProfileAsync(userId, model.SteamUser, model.Nick);
        if (updated == null)
        {
            return BadRequest("No se pudo actualizar el perfil.");
        }

        return Ok("Perfil actualizado con éxito.");
    }
}