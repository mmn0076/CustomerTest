using System.Text;

namespace CustomerTest.IntegrationTests.Helpers;

public static class TestUtils
{
    public static int GenerateRandomNumber() => SeedRandom().Next(1, 10000);
    public static string GenerateRandomEmail() => $"test{GenerateRandomNumber()}@integrationtest.com";
    public static string GenerateRandomString(int length) => new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz0123456789", length).Select(s => s[new Random().Next(s.Length)]).ToArray())+"_integrationtest";

    public static string GenerateRandomPhoneNumber()
    {
        var sb = new StringBuilder();
        var rnd = SeedRandom();
        for (var i = 0; i < 8; i++) {
            sb.Append(rnd.Next(0, 9));
        }
        return sb.ToString();
    }
    private static Random SeedRandom() {
        return new Random(Guid.NewGuid().GetHashCode());
    }
}