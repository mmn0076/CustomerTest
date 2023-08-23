namespace CustomerTest.Presentation.Contracts.Order;

public class CreateOrderRequest
{
    public double Price { get; set; }
    
    public string CustomerId { get; set; }

}