using System.ComponentModel.DataAnnotations;

public class BeamCornerModel
{
    [Key]
    public string IdBeamCorner { get; set; }= "";
    public PositionModel Position { get; set; } = new PositionModel();
    public BeamModel? Beam { get; set; }
}