namespace CustomerTest.Application.Services.Customer.Common
{
    public class GetCustomerListResult
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }
    }
}