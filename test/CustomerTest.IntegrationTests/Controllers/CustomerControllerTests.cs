using System.Net;
using System.Net.Http.Json;
using CustomerTest.Domain;
using CustomerTest.Domain.Common.Errors;
using CustomerTest.IntegrationTests.Abstraction;
using CustomerTest.IntegrationTests.Helpers;
using CustomerTest.Presentation.Contracts.Customer;
using CustomerTest.Presentation.Contracts.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerTest.IntegrationTests.Controllers;

//Naming Pattern => [MethodBeingTested]_[TestCondition]_Should_[ExpectedResult]
[Collection("Customer")]
public class CustomerControllerTests : BaseIntegrationTest
{
    public CustomerControllerTests(WebAppFactory factory) : base(factory)
    {
    }

    #region CreateCustomer

    [Fact]
    public async Task Create_OnCorrectInfo_Should_InsertToDbAndReturnCreateCustomerResponse()
    {
        //arrange
        var customer = new CreateCustomerRequest()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };

        //act
        var request = await Client.PostAsync(HttpHelper.Urls.CreateCustomer, HttpHelper.GetJsonHttpContent(customer));
        var result = await request.Content.ReadFromJsonAsync<CreateCustomerResponse>();

        //assert
        Assert.True(request.StatusCode == HttpStatusCode.OK);
        Assert.NotEmpty(result!.Id!);
        Assert.Equal(customer.FirstName, result.FirstName);
        Assert.Equal(customer.LastName, result.LastName);
        Assert.True(DbContext.Customers.Any(x => x.Id.ToString() == result.Id));
    }
    
    [Fact]
    public async Task Create_OnDuplicateEmail_Should_ReturnBadRequestWithDuplicateUserMessage()
    {
        //arrange
        var customer = new Domain.Customer()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };
        
        var customerRequest = new CreateCustomerRequest()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = customer.Email,
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };

        await AddEntityAsync(customer);

        //act
        var request = await Client.PostAsync(HttpHelper.Urls.CreateCustomer, HttpHelper.GetJsonHttpContent(customerRequest));
        var result = await request.Content.ReadFromJsonAsync<ProblemDetails>();

        //assert
        Assert.True(request.StatusCode == HttpStatusCode.Conflict);
        Assert.Equal(Errors.Customer.DuplicateUser.Description, result?.Title);
    }
    
    [Fact]
    public async Task Create_OnInvalidEmail_Should_ReturnBadRequestWithEmailIsNotValidMessage()
    {
        //arrange
        var customer = new CreateCustomerRequest()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = "invalid",
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };

        //act
        var request = await Client.PostAsync(HttpHelper.Urls.CreateCustomer, HttpHelper.GetJsonHttpContent(customer));
        var result = await request.Content.ReadFromJsonAsync<ProblemDetails>();

        //assert
        Assert.True(request.StatusCode == HttpStatusCode.BadRequest);
        Assert.Equal(Errors.Validation.EmailIsNotValid.Description, result?.Title);
    }
    
    [Fact]
    public async Task Create_OnNullFirstName_Should_ReturnBadRequestWithFirstNameRequiredMessage()
    {
        //arrange
        var customer = new CreateCustomerRequest()
        {
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };

        //act
        var request = await Client.PostAsync(HttpHelper.Urls.CreateCustomer, HttpHelper.GetJsonHttpContent(customer));
        var result = await request.Content.ReadFromJsonAsync<ProblemDetails>();

        //assert
        Assert.True(request.StatusCode == HttpStatusCode.BadRequest);
        Assert.Equal(Errors.Validation.FirstNameRequired.Description, result?.Title);
    }
    
    [Fact]
    public async Task Create_OnNullLastName_Should_ReturnBadRequestWithLastNameRequiredMessage()
    {
        //arrange
        var customer = new CreateCustomerRequest()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };

        //act
        var request = await Client.PostAsync(HttpHelper.Urls.CreateCustomer, HttpHelper.GetJsonHttpContent(customer));
        var result = await request.Content.ReadFromJsonAsync<ProblemDetails>();

        //assert
        Assert.True(request.StatusCode == HttpStatusCode.BadRequest);
        Assert.Equal(Errors.Validation.LastNameRequired.Description, result?.Title);
    }
    
    [Fact]
    public async Task Create_OnInValidPhoneNumber_Should_ReturnBadRequestWithMessage()
    {
        //arrange
        var customer = new CreateCustomerRequest()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = "0",
            DateOfBirth = new DateTime(1997, 7, 7)
        };

        //act
        var request = await Client.PostAsync(HttpHelper.Urls.CreateCustomer, HttpHelper.GetJsonHttpContent(customer));
        var result = await request.Content.ReadFromJsonAsync<ProblemDetails>();

        //assert
        Assert.True(request.StatusCode == HttpStatusCode.BadRequest);
        Assert.NotEmpty(result?.Title!);
    }

    #endregion

    #region GetCustomer

    [Fact]
    public async Task Get_OnExistCustomer_Should_ReturnCustomerDetails()
    {
        //Arrange
        var customer = new Customer()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };
        
        await AddEntityAsync(customer);
        
        //Act
        HttpResponseMessage request = await Client.GetAsync(HttpHelper.Urls.GetCustomer + $"/{customer.Id}");
        var result = await request.Content.ReadFromJsonAsync<GetCustomerResponse>();

        //Assert
        Assert.True(request.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(result);
        Assert.Equal(customer.Id.ToString(),result.Id);
        Assert.Equal(customer.FirstName,result.FirstName);
        Assert.Equal(customer.LastName,result.LastName);
        Assert.Equal(customer.Address,result.Address);
        Assert.Equal(customer.Email,result.Email);
        Assert.Equal(customer.PhoneNumber,result.PhoneNumber);
        Assert.Equal(customer.DateOfBirth,result.DateOfBirth);
        
    }
    
    [Fact]
    public async Task Get_OnNotExistCustomer_Should_ReturnNotFound()
    {
        //Act
        HttpResponseMessage request = await Client.GetAsync(HttpHelper.Urls.GetCustomer + $"/{Guid.NewGuid()}");
        var result = await request.Content.ReadFromJsonAsync<ProblemDetails>();

        //Assert
        Assert.True(request.StatusCode == HttpStatusCode.NotFound);
        Assert.Equal(Errors.Customer.NotFound.Description, result?.Title);
    }
    
    #endregion

    #region ListCustomer
    
    [Fact]
    public async Task List_OnExistCustomers_Should_ReturnCustomersDetails()
    {
        //Arrange
        var customer1 = new Customer()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };
        var customer2 = new Customer()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };
        
        await AddEntityAsync(customer1);
        await AddEntityAsync(customer2);
        
        var requestData = new GetCustomersRequst() { Limit = 2 , Offset = 0};
        
        //Act
        HttpResponseMessage request = await Client.GetAsync(HttpHelper.Urls.ListCustomer + "?" + ToQueryString(requestData));
        var result = await request.Content.ReadFromJsonAsync<List<GetCustomerListResponse>>();

        //Assert
        Assert.True(request.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(result);
        Assert.True(result.Count == 2);

    }
    
    #endregion

    #region EditCustomer

    [Fact]
    public async Task Put_OnExistingCustomer_Should_EditCustomerAndReturnDetails()
    {
        //Arrange
        var customer = new Customer()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };
        
        await AddEntityAsync(customer);
        
        var requestData = new EditCustomerRequest()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };

        //Act
        HttpResponseMessage request = await Client.PutAsync(HttpHelper.Urls.EditCustomer + $"/{customer.Id}", HttpHelper.GetJsonHttpContent(requestData));
        var result = await request.Content.ReadFromJsonAsync<EditCustomerResponse>();

        //Assert
        Assert.True(request.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(result);
        Assert.Equal(requestData.FirstName,result.FirstName);
        Assert.Equal(requestData.LastName,result.LastName);
        Assert.Equal(requestData.Address,result.Address);
        Assert.Equal(requestData.Email,result.Email);
        Assert.Equal(requestData.PhoneNumber,result.PhoneNumber);
        Assert.Equal(requestData.DateOfBirth,result.DateOfBirth);
        await DbContext.Entry(customer).ReloadAsync();
        Assert.Equal(requestData.FirstName,customer.FirstName);
        Assert.Equal(requestData.LastName,customer.LastName);
        Assert.Equal(requestData.Address,customer.Address);
        Assert.Equal(requestData.Email,customer.Email);
        Assert.Equal(requestData.PhoneNumber,customer.PhoneNumber);
        Assert.Equal(requestData.DateOfBirth,customer.DateOfBirth);
    }
    
     [Fact]
    public async Task Put_OnDuplicateEmail_Should_ReturnBadRequestWithDuplicateUserMessage()
    {
        //Arrange
        var customer = new Customer()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };
        
        var customer2 = new Customer()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };
        
        await AddEntityAsync(customer);
        await AddEntityAsync(customer2);
        
        var requestData = new EditCustomerRequest()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = customer2.Email,
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };

        //Act
        HttpResponseMessage request = await Client.PutAsync(HttpHelper.Urls.EditCustomer + $"/{customer.Id}", HttpHelper.GetJsonHttpContent(requestData));
        var result = await request.Content.ReadFromJsonAsync<ProblemDetails>();

        //assert
        Assert.True(request.StatusCode == HttpStatusCode.Conflict);
        Assert.Equal(Errors.Customer.DuplicateUser.Description, result?.Title);
    }
    
    [Fact]
    public async Task Put_OnNotExistCustomer_Should_ReturnNotFound()
    {
        //Arrange
        var requestData = new EditCustomerRequest()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };
        
        //Act
        HttpResponseMessage request = await Client.PutAsync(HttpHelper.Urls.EditCustomer + $"/{Guid.NewGuid()}", HttpHelper.GetJsonHttpContent(requestData));
        var result = await request.Content.ReadFromJsonAsync<ProblemDetails>();

        //Assert
        Assert.True(request.StatusCode == HttpStatusCode.NotFound);
        Assert.Equal(Errors.Customer.NotFound.Description, result?.Title);
    }
    
    #endregion

    #region DeleteCustomer

    [Fact]
    public async Task Delete_OnExistingCustomer_Should_RemoveCustomer()
    {
        //Arrange
        var customer = new Customer()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7)
        };
        
        await AddEntityAsync(customer);
        
        //Act
        HttpResponseMessage request = await Client.DeleteAsync(HttpHelper.Urls.DeleteCustomer + $"/{customer.Id}");

        //Assert
        Assert.True(request.StatusCode == HttpStatusCode.OK);
        Assert.False(await DbContext.Customers.AnyAsync(x => x.Id == customer.Id));

    }
    
    [Fact]
    public async Task Delete_OnNotExistCustomer_Should_ReturnNotFound()
    {
        //Act
        HttpResponseMessage request = await Client.GetAsync(HttpHelper.Urls.DeleteCustomer + $"/{Guid.NewGuid()}");
        var result = await request.Content.ReadFromJsonAsync<ProblemDetails>();

        //Assert
        Assert.True(request.StatusCode == HttpStatusCode.NotFound);
        Assert.Equal(Errors.Customer.NotFound.Description, result?.Title);
    }
    
    #endregion

    #region GetCustomerOrders

    [Fact]
    public async Task OrdersList_OnExistCustomer_Should_ReturnCustomerDetails()
    {
        //Arrange
        var customer = new Customer()
        {
            FirstName = TestUtils.GenerateRandomString(5),
            LastName = TestUtils.GenerateRandomString(5),
            Address = TestUtils.GenerateRandomString(15),
            Email = TestUtils.GenerateRandomEmail(),
            PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
            DateOfBirth = new DateTime(1997, 7, 7),
            Orders = new List<Order>()
            {
                new ()
                {
                    Price = 1200,
                    CreateDate = DateTimeOffset.UtcNow,
                },
                new ()
                {
                    Price = 3000,
                    CreateDate = DateTimeOffset.UtcNow,
                }
            }
        };
        
        await AddEntityAsync(customer);
        
        //Act
        HttpResponseMessage request = await Client.GetAsync(HttpHelper.Urls.OrdersList + $"/{customer.Id}/Orders");
        var result = await request.Content.ReadFromJsonAsync<List<GetOrderResponse>>();

        //Assert
        Assert.True(request.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(result);
        Assert.True(result.Count == 2);
        Assert.Contains(result, x => x.Price == customer.Orders[0].Price);
        Assert.Contains(result, x => x.Price == customer.Orders[1].Price);
        
    }

    #endregion
    
    
}