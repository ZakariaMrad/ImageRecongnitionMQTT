using Emgu.CV.Util;

public class MarkerModel
{
    public string IdMarker { get; set; }
    public PositionModel Position { get; set; } = new PositionModel();

    public MarkerModel(int id, VectorOfPointF corners)
    {
        IdMarker = id.ToString();
        //Position is the center of the marker
        Position.IdPosition = id;
        Position.X = (int)(corners[0].X + corners[1].X + corners[2].X + corners[3].X) / 4;
        Position.Y = (int)(corners[0].Y + corners[1].Y + corners[2].Y + corners[3].Y) / 4;
    }

    internal List<PositionModel> ToPosition()
    {
        throw new NotImplementedException();
    }

    public BeamCornerModel ToCorner()
    {
        return new BeamCornerModel
        {
            Position = Position
        };
    }
}