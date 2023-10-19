using System.Text;
using Newtonsoft.Json;

namespace CustomerTest.IntegrationTests.Helpers;

public static class HttpHelper
{

    public static StringContent GetJsonHttpContent(object obj)
    {
        return new StringContent(JsonConvert.SerializeObject(obj),Encoding.UTF8,"application/json");
    }

    internal static class Urls
    {
        //Customer
        public const string CreateCustomer = "api/v1/Customer";
        public const string GetCustomer = "api/v1/Customer";
        public const string EditCustomer = "api/v1/Customer";
        public const string ListCustomer = "api/v1/Customer/List";
        public const string DeleteCustomer = "api/v1/Customer";
        public const string OrdersList = "api/v1/Customer";
        
        //Order
        public const string CreateOrder = "api/v1/Order";
        public const string GetOrder = "api/v1/Order";
        public const string EditOrder = "api/v1/Order";
        public const string DeleteOrder = "api/v1/Order";
    }
    
}