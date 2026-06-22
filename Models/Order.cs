namespace ShepherdsPie.Models;

public class Order
{
    public int Id { get; set; }

    // Null when the order is for delivery; set to a table number for dine-in.
    public int? TableNumber { get; set; }

    // Null when the customer did not leave a tip.
    public decimal? Tip { get; set; }

    // When the order was placed, for record-keeping.
    public DateTime OrderDate { get; set; }

    // The employee who took the order (at a table or over the phone). Always required.
    public int EmployeeId { get; set; }
    public UserProfile Employee { get; set; }

    // The employee assigned to deliver the order. Null = not a delivery order.
    public int? DelivererId { get; set; }
    public UserProfile Deliverer { get; set; }

    // The pizzas on this order.
    public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
}
