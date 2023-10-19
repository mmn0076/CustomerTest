using System.Net;
using System.Net.Http.Json;
using CustomerTest.Domain;
using CustomerTest.Domain.Common.Errors;
using CustomerTest.IntegrationTests.Abstraction;
using CustomerTest.IntegrationTests.Helpers;
using CustomerTest.Presentation.Contracts.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerTest.IntegrationTests.Controllers;

[Collection("Order")]
public class OrderControllerTests : BaseIntegrationTest
{
    public OrderControllerTests(WebAppFactory factory) : base(factory)
    {
    }
    
     #region CreateOrder

    [Fact]
    public async Task Create_OnCorrectInfo_Should_InsertToDbAndReturnCreateOrderResponse()
    {
        //arrange
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
        
        
        var order = new CreateOrderRequest()
        {
           Price = TestUtils.GenerateRandomNumber(),
           CustomerId = customer.Id.ToString()
        };

        //act
        var request = await Client.PostAsync(HttpHelper.Urls.CreateOrder, HttpHelper.GetJsonHttpContent(order));
        var result = await request.Content.ReadFromJsonAsync<CreateOrderReponse>();

        //assert
        Assert.True(request.StatusCode == HttpStatusCode.OK);
        Assert.NotEmpty(result!.Id!);
        Assert.Equal(order.Price, result.Price);
        Assert.Equal(order.CustomerId, result.CustomerId);
        Assert.True(DbContext.Orders.Any(x => x.Id.ToString() == result.Id));
    }
   
    [Fact]
    public async Task Create_OnNullPrice_Should_ReturnBadRequestWithEmailIsNotValidMessage()
    {
        //arrange
        var order = new CreateOrderRequest()
        {
            CustomerId = Guid.NewGuid().ToString()
        };

        //act
        var request = await Client.PostAsync(HttpHelper.Urls.CreateOrder, HttpHelper.GetJsonHttpContent(order));
        var result = await request.Content.ReadFromJsonAsync<ProblemDetails>();

        //assert
        Assert.True(request.StatusCode == HttpStatusCode.BadRequest);
        Assert.Equal(Errors.Validation.PriceRequired.Description, result?.Title);
    }

    #endregion

    #region GetOrder

    [Fact]
    public async Task Get_OnExistOrder_Should_ReturnOrderDetails()
    {
        //arrange
        var order = new Order()
        {
            Price = TestUtils.GenerateRandomNumber(),
            CreateDate = DateTimeOffset.UtcNow,
            EditDate = DateTimeOffset.UtcNow.AddDays(1),
            Customer = new Customer()
            {
                FirstName = TestUtils.GenerateRandomString(5),
                LastName = TestUtils.GenerateRandomString(5),
                Address = TestUtils.GenerateRandomString(15),
                Email = TestUtils.GenerateRandomEmail(),
                PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
                DateOfBirth = new DateTime(1997, 7, 7),
            }
        };
        
        await AddEntityAsync(order);
        
        //Act
        HttpResponseMessage request = await Client.GetAsync(HttpHelper.Urls.GetOrder + $"/{order.Id}");
        var result = await request.Content.ReadFromJsonAsync<GetOrderResponse>();

        //Assert
        Assert.True(request.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(result);
        Assert.Equal(order.Id.ToString(),result.Id);
        Assert.Equal(order.Price,result.Price);
        Assert.Equal(order.CreateDate,result.CreateDate);
        Assert.Equal(order.EditDate,result.EditDate);

        
    }
    
    [Fact]
    public async Task Get_OnNotExistOrder_Should_ReturnNotFound()
    {
        //Act
        HttpResponseMessage request = await Client.GetAsync(HttpHelper.Urls.GetOrder + $"/{Guid.NewGuid()}");
        var result = await request.Content.ReadFromJsonAsync<ProblemDetails>();

        //Assert
        Assert.True(request.StatusCode == HttpStatusCode.NotFound);
        Assert.Equal(Errors.Order.NotFound.Description, result?.Title);
    }
    
    #endregion

    #region EditOrder

    [Fact]
    public async Task Put_OnExistingOrder_Should_EditOrderAndReturnDetails()
    {
        //arrange
        var order = new Order()
        {
            Price = TestUtils.GenerateRandomNumber(),
            CreateDate = DateTimeOffset.UtcNow,
            EditDate = DateTimeOffset.UtcNow.AddDays(1),
            Customer = new Customer()
            {
                FirstName = TestUtils.GenerateRandomString(5),
                LastName = TestUtils.GenerateRandomString(5),
                Address = TestUtils.GenerateRandomString(15),
                Email = TestUtils.GenerateRandomEmail(),
                PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
                DateOfBirth = new DateTime(1997, 7, 7),
            }
        };
        
        await AddEntityAsync(order);
        
        var requestData = new EditOrderRequest()
        {
            Price = TestUtils.GenerateRandomNumber(),
        };

        //Act
        HttpResponseMessage request = await Client.PutAsync(HttpHelper.Urls.EditOrder + $"/{order.Id}", HttpHelper.GetJsonHttpContent(requestData));
        var result = await request.Content.ReadFromJsonAsync<EditOrderResponse>();

        //Assert
        Assert.True(request.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(result);
        Assert.Equal(order.Id.ToString(),result.Id);
        Assert.Equal(requestData.Price,result.Price);
        await DbContext.Entry(order).ReloadAsync();
        Assert.Equal(requestData.Price,order.Price);
        Assert.Equal(DateTimeOffset.UtcNow.Date,order.EditDate.Date);

    }
    
    #endregion

    #region DeleteOrder

    [Fact]
    public async Task Delete_OnExistingOrder_Should_RemoveOrder()
    {
        //arrange
        var order = new Order()
        {
            Price = TestUtils.GenerateRandomNumber(),
            CreateDate = DateTimeOffset.UtcNow,
            EditDate = DateTimeOffset.UtcNow.AddDays(1),
            Customer = new Customer()
            {
                FirstName = TestUtils.GenerateRandomString(5),
                LastName = TestUtils.GenerateRandomString(5),
                Address = TestUtils.GenerateRandomString(15),
                Email = TestUtils.GenerateRandomEmail(),
                PhoneNumber = TestUtils.GenerateRandomPhoneNumber(),
                DateOfBirth = new DateTime(1997, 7, 7),
            }
        };
        
        await AddEntityAsync(order);
        
        //Act
        HttpResponseMessage request = await Client.DeleteAsync(HttpHelper.Urls.DeleteOrder + $"/{order.Id}");

        //Assert
        Assert.True(request.StatusCode == HttpStatusCode.OK);
        Assert.False(await DbContext.Orders.AnyAsync(x => x.Id == order.Id));

    }
    
    #endregion
    
}