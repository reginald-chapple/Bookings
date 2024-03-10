namespace Bookings.Web.Domain;

public class ActionItem : AuditableEntity
{
     public long Id { get; set; }

    public string Label { get; set; } = string.Empty;

    public bool IsCompleted { get; set; } = false;

    public DateTime? CompletionDate { get; set; }

    public long MilestoneId { get; set; }
    public virtual Milestone? Milestone { get; set; }
}