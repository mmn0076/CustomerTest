namespace CustomerTest.Presentation.Contracts.Order;

public class GetOrderResponse
{
    public string? Id { get; set; }
    
    public double Price { get; set; }
    
    public DateTimeOffset CreateDate { get; set; }
    
    public DateTimeOffset EditDate { get; set; }

}