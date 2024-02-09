using System.ComponentModel.DataAnnotations;

public class PositionModel
{
    [Key]
    public int IdPosition { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

}