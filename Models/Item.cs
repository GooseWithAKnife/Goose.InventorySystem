namespace GooseInventorySystem.Models;

public class Item
{
    public int Id { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Company { get; set; }
    public string? Location { get; set; }
    public int Quantity { get; set; }
}
