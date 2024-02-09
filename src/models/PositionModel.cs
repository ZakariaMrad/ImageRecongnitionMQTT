using System.ComponentModel.DataAnnotations;
using System.Drawing;

public class PositionModel
{
    [Key]
    public int IdPosition { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Point ToPoint()
    {
        return new Point(X, Y);
    }
}