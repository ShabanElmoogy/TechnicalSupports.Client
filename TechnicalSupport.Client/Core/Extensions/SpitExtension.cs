namespace TechnicalSupport.Client.Core.Extensions;

public static partial class SpitExtension
{
    public static string SplitCamelCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        // Insert a space before each uppercase letter (except the first letter)
        return MyRegex().Replace(str, "$1 $2");
    }

    [System.Text.RegularExpressions.GeneratedRegex("([a-z])([A-Z])")]
    private static partial System.Text.RegularExpressions.Regex MyRegex();
}
