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
    public bool? CanBeSaved { get; set; }
}