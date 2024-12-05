using Gametopia.Contracts.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserRelationsController : ControllerBase
{
    private readonly UserRelationService _userRelationService;

    public UserRelationsController(UserRelationService userRelationService)
    {
        _userRelationService = userRelationService;
    }

    [HttpPost("{targetUserId}/block")]
    public async Task<IActionResult> BlockUser(string targetUserId)
    {
        var sourceUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _userRelationService.BlockUserAsync(sourceUserId, targetUserId);
        return Ok();
    }

    [HttpPost("{targetUserId}/unblock")]
    public async Task<IActionResult> UnblockUser(string targetUserId)
    {
        var sourceUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _userRelationService.UnblockUserAsync(sourceUserId, targetUserId);
        return Ok();
    }

    [HttpPost("{targetUserId}/follow")]
    public async Task<IActionResult> FollowUser(string targetUserId)
    {
        var sourceUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _userRelationService.FollowUserAsync(sourceUserId, targetUserId);
        return Ok();
    }

    [HttpPost("{targetUserId}/unfollow")]
    public async Task<IActionResult> UnfollowUser(string targetUserId)
    {
        var sourceUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _userRelationService.UnfollowUserAsync(sourceUserId, targetUserId);
        return Ok();
    }

    [HttpPost("{targetUserId}/report")]
    public async Task<IActionResult> ReportUser(string targetUserId, [FromBody] ReportDto report)
    {
        var sourceUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _userRelationService.ReportUserAsync(sourceUserId, targetUserId, report.Reason, report.Comment);
        return Ok();
    }
}