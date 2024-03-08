namespace Bookings.Web.Domain;

public class ActionItem : Entity
{
     public long Id { get; set; }

    public string Label { get; set; } = string.Empty;

    public bool IsCompleted { get; set; } = false;

    public DateTime? CompletionDate { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime? DeleteDate { get; set; }

    public long MilestoneId { get; set; }
    public virtual Milestone? Milestone { get; set; }
}