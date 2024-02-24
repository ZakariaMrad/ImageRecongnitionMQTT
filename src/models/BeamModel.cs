using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImageRecognitionMQTT.Enums;

public class BeamModel
{
    [Key]
    public string IdBeam { get; set; } = "";
    public string MarkerValue { get; set; } = "";
    public string? MarkerValueBase64 { get; set; } = null;
    public string Name { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool CanBeSaved { get; set; } = true;
    public List<ItemModel> Items { get; set; } = new List<ItemModel>();
    public string Href { get; set; } = "";
    public List<BeamItemModel> BeamItems { get; set; } = new List<BeamItemModel>();
}
