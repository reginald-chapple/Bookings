using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bookings.Web.Domain;

public class MatchProfile : Entity
{
    public long Id { get; set; }

    // public DateOnly DateOfBirth { get; set; }

    // public double HeightInInches { get; set; }

    // public double Weight { get; set; }

    public bool HasChildren { get; set; }

    public bool IsSmoker { get; set; }

    public bool IsDrinker { get; set; }

    [Display(Name = "Hair color")]
    public HairColor HairColor { get; set; }

    [Display(Name = "Eye color")]
    public EyeColor EyeColor { get; set; }

    [Display(Name = "Body type")]
    public BodyType BodyType { get; set; }

    public Sexuality Sexuality { get; set; }

    public int? ComplexionId { get; set; }
    public virtual SkinComplexion? Complexion { get; set; }

    [Display(Name = "Gender identity")]
    public GenderIdentity GenderIdentity { get; set; }

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser? User { get; set; }

    // public ICollection<ProfileAttraction> Attractions { get; set; } = [];
}

public enum HairColor
{
    [Description("Black")]
    Black,
    [Description("Brown")]
    Brown,
    [Description("Auburn")]
    Auburn,
    [Description("Red")]
    Red,
    [Description("Blond")]
    Blond,
    [Description("Gray")]
    Gray,
    [Description("White")]
    White
}

public enum EyeColor
{
    [Description("Brown")]
    Brown,
    [Description("Amber")]
    Amber,
    [Description("Hazel")]
    Hazel,
    [Description("Green")]
    Green,
    [Description("Blue")]
    Blue,
    [Description("Gray")]
    Gray,
    [Description("Mixed")]
    Mixed
}

public enum BodyType
{
    [Description("Lean")]
    Lean,
    [Description("Muscular")]
    Muscular,
    [Description("Curvy")]
    Curvy
}

public enum Complexion
{
    [Description("Porcelain")]
    Porcelain,
    [Description("Ivory")]
    Ivory,
    [Description("Honey")]
    Honey,
    [Description("Olive")]
    Olive,
    [Description("Brown")]
    Brown,
    [Description("Ebony")]
    Ebony
}

public enum GenderIdentity
{
    [Description("Female")]
    Female,
    [Description("Transgender female")]
    TransFemale,
    [Description("Male")]
    Male,
    [Description("Transgender male")]
    TransMale,
    [Description("Trans")]
    Trans
}

public enum Sexuality
{
    [Description("Heterosexual female")]
    HeteroFemale,
    [Description("Heterosexual male")]
    HeteroMale,
    [Description("Transgender female")]
    TransFemale,
    [Description("Transgender male")]
    TransMale,
    [Description("Bisexual female")]
    BiFemale,
    [Description("Bisexual male")]
    BiMale,
    [Description("Homosexual female")]
    HomoFemale,
    [Description("Homosexual male")]
    HomoMale,
    [Description("Non-binary")]
    NonBinary
}