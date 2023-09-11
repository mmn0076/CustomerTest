using System.Net;
using System.Net.Http.Json;
using CustomerTest.EndToEndTests.Helpers;
using CustomerTest.Presentation.Contracts.Customer;

namespace CustomerTest.EndToEndTests.Customer;

public class CreateCustomerTests : BaseEndToEndTest
{
    public CreateCustomerTests(WebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task CreateCustomer_CorrectInfo_ShouldInsertToDb()
    {
        //arrange
        var customer = new CreateCustomerRequest()
        {
          FirstName = "FirstNameTest",
          LastName = "LastNameTest",
          Address = "TestAddress",
          Email = "test@gmail.com",
          PhoneNumber = "09175681142",
          DateOfBirth = new DateTime(1997,7,7)
        };

        //act
        var request = await Client.PostAsync(HttpHelper.Urls.CreateCustomer, HttpHelper.GetJsonHttpContent(customer));
        var result = await request.Content.ReadFromJsonAsync<CreateCustomerResponse>();
        
        //assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        Assert.NotEmpty(result!.Id);
        Assert.Equal(customer.FirstName,result.FirstName);
        Assert.True(DbContext.Customers.Any(x => x.Id.ToString() == result.Id));
    }


}