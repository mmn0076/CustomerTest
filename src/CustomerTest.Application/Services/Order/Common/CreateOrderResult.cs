namespace CustomerTest.Application.Services.Order.Common;

public class CreateOrderResult
{
    public Guid Id { get; set; }
    
    public double Price { get; set; }
    
    public string? CustomerId { get; set; }
    
    public DateTimeOffset CreateDate { get; set; }
}