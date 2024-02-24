using System.ComponentModel.DataAnnotations;

public class ItemModel
{
    [Key]
    public string IdItem { get; set; } = "";
    public string Name { get; set; } = "";
    public string Barcode { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string MarkerValue { get; set; } = "";
    public string? MarkerValueBase64 { get; set; } = null;
    public List<BeamModel> Beams { get; set; } = new List<BeamModel>();
    public List<BeamItemModel> beamItems { get; set; } = new List<BeamItemModel>();
    public string Href { get; set; } = "";
}
