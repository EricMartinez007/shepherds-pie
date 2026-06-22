namespace ShepherdsPie.Models.DTOs;

public class PizzaDTO
{
    public int Id { get; set; }
    public int OrderId { get; set; }

    public int SizeId { get; set; }
    public SizeDTO Size { get; set; }

    public int CheeseId { get; set; }
    public CheeseDTO Cheese { get; set; }

    public int SauceId { get; set; }
    public SauceDTO Sauce { get; set; }

    // Flattened from the PizzaTopping join table when mapping.
    public List<ToppingDTO> Toppings { get; set; } = new List<ToppingDTO>();

    // Computed: base size price + the price of every topping on the pizza.
    // Lives on the DTO (not the entity) because it depends on loaded relationships.
    public decimal Total
    {
        get
        {
            decimal sizePrice = Size?.Price ?? 0m;
            decimal toppingsPrice = Toppings.Sum(t => t.Price);
            return sizePrice + toppingsPrice;
        }
    }
}

public class UpdatePizzaDTO
{
    public int SizeId { get; set; }
    public int CheeseId { get; set; }
    public int SauceId { get; set; }
    public List<int> ToppingIds { get; set; } = new();
}

public class CreatePizzaDTO
{
    public int OrderId { get; set; }
    public int SizeId { get; set; }
    public int CheeseId { get; set; }
    public int SauceId { get; set; }
    public List<int> ToppingIds { get; set; } = new();
}

