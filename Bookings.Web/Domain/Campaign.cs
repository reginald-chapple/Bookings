using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Domain;

public class Campaign : Entity
{
    public Campaign() {}
    
    public long Id { get; set; }

    public string Slug { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public bool IsPublished { get; set; } = false;

    [Precision(9, 2)]
    [Display(Name = "Fundraising goal")]
    public decimal FundraisingGoal { get; set; } = 0.0m;

    [DataType(DataType.Text)]
    public string Problem { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Goal { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Beneficiaries { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Importance { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string Solution { get; set; } = string.Empty;

    public ProgressStatus Status { get; set; } = ProgressStatus.Draft;

    public bool IsDeleted { get; set; } = false;

    public DateTime? DeleteDate { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? Deadline { get; set; }

    public DateTime? PublishDate { get; set; }

    public string ManagerId { get; set; } = string.Empty;
    public virtual ApplicationUser? Manager { get; set; }

    public long CauseId { get; set; }
    public virtual Cause? Cause { get; set; }

    // public long CityId { get; set; }
    // public virtual City? City { get; set; }
    public virtual ICollection<Milestone> Milestones { get; set; } = [];
    public virtual ICollection<Opportunity> Opportunities { get; set; } = [];
    public virtual ICollection<Donation> Donations { get; set; } = [];
    public virtual ICollection<Expenditure> Expenditures { get; set; } = [];
    public virtual ICollection<TimelineEntry> Timeline { get; set; } = [];

    public int DaysPassed()
    {
        if (PublishDate.HasValue)
        {
            return (DateTime.Now.Date - PublishDate.Value.Date).Days;
        }
        return 0; 
    }

    public decimal Fundsraised()
    {
        return Donations.Sum(d => d.Amount);
    }

    public decimal FundsNeeded()
    {
        return FundraisingGoal - Donations.Sum(d => d.Amount);
    }
}