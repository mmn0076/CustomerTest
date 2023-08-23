namespace CustomerTest.Presentation.Contracts.Order;

public class CreateOrderReponse
{
    public string Id { get; set; }
    public double Price { get; set; }
    public string CustomerId { get; set; }
    public DateTimeOffset CreateDate { get; set; }

}