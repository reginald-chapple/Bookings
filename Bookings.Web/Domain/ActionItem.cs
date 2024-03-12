using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Domain;

public class ActionItem : AuditableEntity
{
     public long Id { get; set; }

    public string Label { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Details { get; set; } = string.Empty;

    [Precision(5, 2)]
    public decimal PercentageComplete { get; set; } = 0.0m;

    public DateTime? Deadline { get; set; }

    public bool IsCompleted { get; set; } = false;

    public DateTime? CompletionDate { get; set; }

    public ActionItemType Type { get; set; }

    public long CampaignId { get; set; }
    public virtual Campaign? Campaign { get; set; }

    public long? ParentId { get; set; }
    public virtual ActionItem? Parent { get; set; }

    public virtual ICollection<ActionItem> Children { get; set; } = [];
}

public enum ActionItemType
{
    [Description("Milestone")]
    Milestone,
    [Description("Task")]
    Task
}