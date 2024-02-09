using Emgu.CV.Util;

public class MarkerModel
{
    public int Id { get; set; }
    public Position Position { get; set; } = new Position();

    public MarkerModel(int id, VectorOfPointF corners)
    {
        Id = id;
        //Position is the center of the marker
        Position.X = (int)(corners[0].X + corners[1].X + corners[2].X + corners[3].X) / 4;
        Position.Y = (int)(corners[0].Y + corners[1].Y + corners[2].Y + corners[3].Y) / 4;
    }
}