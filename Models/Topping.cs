namespace ShepherdsPie.Models;

public class Topping
{
    public int Id { get; set; }

    // e.g. "sausage", "pepperoni", "mushroom"
    public string Name { get; set; }

    // Price per topping ($0.50 on the opening menu).
    public decimal Price { get; set; }

    // The pizza-topping links that reference this topping.
    public List<PizzaTopping> PizzaToppings { get; set; } = new List<PizzaTopping>();
}
