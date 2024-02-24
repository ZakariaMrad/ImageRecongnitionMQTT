using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

public class Esp32Model
{
    [Key]
    public string IdEsp32 { get; set; } = "";
    public string Token { get; set; } = "";
    public string MacAddress { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Href { get; set; } = "";
}
