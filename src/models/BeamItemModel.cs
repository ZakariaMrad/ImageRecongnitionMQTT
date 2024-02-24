using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BeamItemModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string IdBeamItem { get; set; } = "";
    public string IdItem { get; set; } = "";
    public string IdBeam { get; set; } = "";
    public string IdImage { get; set; } = "";
    public DateTime seenAt { get; set; } = DateTime.Now;
}
