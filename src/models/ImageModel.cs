using System.ComponentModel.DataAnnotations;

public class ImageModel
{
    [Key]
    public string IdImage { get; set; } = "";
    public string Path { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime DeleteAt { get; set; }
    public string TakenBy { get; set; } = "";
    public string? AsBase64 { get; set; }
}
