using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ImageRecognitionMQTT.Enums;

public class BeamModel
{
    [Key]
    public string IdBeam { get; set; } = "";
    public string Name { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<ItemModel> Items { get; set; } = new List<ItemModel>();
    public List<PositionModel> Corners { get; set; } = new List<PositionModel>();


    private static Position[]? GetTopBottom(Position[] positions)
    {
        var top = positions.OrderBy(p => p.Y).Take(2).ToArray();
        var bottom = positions.OrderByDescending(p => p.Y).Take(2).ToArray();
        if (top.Length == 2 && bottom.Length == 2)
        {
            return new[] { top[0], top[1], bottom[0], bottom[1] };
        }
        return null;
    }
}