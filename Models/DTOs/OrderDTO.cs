namespace ShepherdsPie.Models.DTOs;

public class OrderDTO
{
    // Surcharge added to any order that has a deliverer assigned.
    public const decimal DeliverySurcharge = 5.00m;

    public int Id { get; set; }
    public int? TableNumber { get; set; }
    public decimal? Tip { get; set; }
    public DateTime OrderDate { get; set; }

    public int EmployeeId { get; set; }
    public UserProfileDTO Employee { get; set; }

    public int? DelivererId { get; set; }
    public UserProfileDTO Deliverer { get; set; }

    public List<PizzaDTO> Pizzas { get; set; } = new List<PizzaDTO>();

    // An order is a delivery order when a deliverer has been assigned.
    public bool IsDelivery => DelivererId.HasValue;

    // Sum of every pizza's total.
    public decimal PizzaTotal => Pizzas.Sum(p => p.Total);

    // Total cost of the order: all pizzas plus the delivery surcharge when applicable.
    // The tip is intentionally not included here — Joe wants to see it separately.
    public decimal Total => PizzaTotal + (IsDelivery ? DeliverySurcharge : 0m);
}
