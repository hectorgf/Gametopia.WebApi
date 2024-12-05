using Gametopia.WebApi.Configuration;
using Microsoft.EntityFrameworkCore;
using static Gametopia.Contracts.Enums.UserRelations;

public class UserRelationService
{
    private readonly GametopiaDbContext _context;

    public UserRelationService(GametopiaDbContext context)
    {
        _context = context;
    }

    public async Task BlockUserAsync(string sourceUserId, string targetUserId)
    {
        var relation = await _context.UserRelations
            .FirstOrDefaultAsync(r => r.SourceUserId == sourceUserId && r.TargetUserId == targetUserId);

        if (relation == null)
        {
            relation = new UserRelation
            {
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId,
                RelationType = UserRelationType.Blocked
            };
            _context.UserRelations.Add(relation);
        }
        else
        {
            relation.RelationType = UserRelationType.Blocked;
        }

        await _context.SaveChangesAsync();
    }

    public async Task UnblockUserAsync(string sourceUserId, string targetUserId)
    {
        var relation = await _context.UserRelations
            .FirstOrDefaultAsync(r => r.SourceUserId == sourceUserId && r.TargetUserId == targetUserId);

        if (relation != null && relation.RelationType == UserRelationType.Blocked)
        {
            _context.UserRelations.Remove(relation);
            await _context.SaveChangesAsync();
        }
    }

    public async Task FollowUserAsync(string sourceUserId, string targetUserId)
    {
        var relation = await _context.UserRelations
            .FirstOrDefaultAsync(r => r.SourceUserId == sourceUserId && r.TargetUserId == targetUserId);

        if (relation == null)
        {
            relation = new UserRelation
            {
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId,
                RelationType = UserRelationType.Followed
            };
            _context.UserRelations.Add(relation);
        }
        else
        {
            relation.RelationType = UserRelationType.Followed;
        }

        await _context.SaveChangesAsync();
    }

    public async Task UnfollowUserAsync(string sourceUserId, string targetUserId)
    {
        var relation = await _context.UserRelations
            .FirstOrDefaultAsync(r => r.SourceUserId == sourceUserId && r.TargetUserId == targetUserId);

        if (relation != null && relation.RelationType == UserRelationType.Followed)
        {
            _context.UserRelations.Remove(relation);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ReportUserAsync(string sourceUserId, string targetUserId, ReportReason reason, string? comment)
    {
        var relation = await _context.UserRelations
            .FirstOrDefaultAsync(r => r.SourceUserId == sourceUserId && r.TargetUserId == targetUserId);

        if (relation == null)
        {
            relation = new UserRelation
            {
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId,
                RelationType = UserRelationType.Reported,
                ReportReason = reason,
                ReportComment = comment
            };
            _context.UserRelations.Add(relation);
        }
        else
        {
            relation.RelationType = UserRelationType.Reported;
            relation.ReportReason = reason;
            relation.ReportComment = comment;
        }

        await _context.SaveChangesAsync();
    }
}