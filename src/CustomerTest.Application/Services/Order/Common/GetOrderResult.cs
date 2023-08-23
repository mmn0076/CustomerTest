namespace CustomerTest.Application.Services.Order.Common;

public class GetOrderResult
{
    public Guid Id { get; set; }
    
    public double Price { get; set; }
    
    public DateTimeOffset CreateDate { get; set; }
    
    public DateTimeOffset EditDate { get; set; }
}