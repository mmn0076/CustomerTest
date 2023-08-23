namespace CustomerTest.Presentation.Contracts.Customer
{
    public record GetCustomersRequst
    {
        public int Offset { get; set; } = 0;
        
        public int Limit { get; set; } = 10;
    }
}
