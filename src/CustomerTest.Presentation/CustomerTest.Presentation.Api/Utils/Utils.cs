namespace CustomerTest.Presentation.Api.Utils;

public static class Utils
{
    public static bool IsGuid(string value)
    {
        Guid x;
        return Guid.TryParse(value, out x);
    }
}