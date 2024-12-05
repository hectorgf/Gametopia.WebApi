using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using static Gametopia.Contracts.Enums.UserRelations;

public class UserRelation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string SourceUserId { get; set; } = null!;
    public IdentityUser SourceUser { get; set; } = null!;

    public string TargetUserId { get; set; } = null!;
    public IdentityUser TargetUser { get; set; } = null!;

    public UserRelationType RelationType { get; set; } = UserRelationType.None;

    public ReportReason? ReportReason { get; set; } = null;
    public string? ReportComment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
