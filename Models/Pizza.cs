namespace ShepherdsPie.Models;

public class Pizza
{
    public int Id { get; set; }

    // The order this pizza belongs to.
    public int OrderId { get; set; }
    public Order Order { get; set; }

    // Size determines the base price of the pizza.
    public int SizeId { get; set; }
    public Size Size { get; set; }

    // Cheese and sauce each have a "None" option, so these are always set.
    public int CheeseId { get; set; }
    public Cheese Cheese { get; set; }

    public int SauceId { get; set; }
    public Sauce Sauce { get; set; }

    // The toppings on this pizza (many-to-many through the PizzaTopping join table).
    public List<PizzaTopping> PizzaToppings { get; set; } = new List<PizzaTopping>();
}
