namespace CustomerTest.Application.Services.Order.Common;

public class EditOrderResult
{
    public Guid Id { get; set; }

    public double Price { get; set; }

    public string CustomerId { get; set; }

    public DateTimeOffset CreateDate { get; set; }

    public DateTimeOffset EditDate { get; set; }
}