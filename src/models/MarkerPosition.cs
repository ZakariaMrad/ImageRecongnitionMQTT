using System.Drawing;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point ToPoint()
    {
        return new Point(X, Y);
    }
}
