using Emgu.CV.Util;

public class MarkerModel
{
    public int Id { get; set; }
    public List<MarkerPosition> Markers { get; set; } = [];

    public MarkerModel(int id, VectorOfPointF corners)
    {
        Id = id;
        for (int i = 0; i < corners.Size; i++)
        {
            Markers.Add(new MarkerPosition { X = (int)corners[i].X, Y = (int)corners[i].Y });
        }
    }
}