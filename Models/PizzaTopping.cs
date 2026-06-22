namespace ShepherdsPie.Models;

// Join table for the many-to-many relationship between Pizza and Topping.
// One row = "this pizza has this topping".
public class PizzaTopping
{
    public int Id { get; set; }

    public int PizzaId { get; set; }
    public Pizza Pizza { get; set; }

    public int ToppingId { get; set; }
    public Topping Topping { get; set; }
}
