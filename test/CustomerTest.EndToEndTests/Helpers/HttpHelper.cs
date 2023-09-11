using System.Text;
using Newtonsoft.Json;

namespace CustomerTest.EndToEndTests.Helpers;

public static class HttpHelper
{

    public static StringContent GetJsonHttpContent(object obj)
    {
        return new StringContent(JsonConvert.SerializeObject(obj),Encoding.UTF8,"application/json");
    }

    internal static class Urls
    {
        public static readonly string CreateCustomer = "api/v1/Customer";
    }
    
}