namespace ShepherdsPie.Models;

public class Size
{
    public int Id { get; set; }

    // e.g. "Small (10\")"
    public string Name { get; set; }

    // Base price for a pizza of this size.
    public decimal Price { get; set; }
}
