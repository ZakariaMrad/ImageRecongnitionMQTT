using System.ComponentModel.DataAnnotations;

public class ItemModel {
    [Key]
    public string IdItem { get; set; } = "";
    public string Name { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}