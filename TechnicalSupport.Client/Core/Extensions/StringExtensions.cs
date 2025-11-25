namespace TechnicalSupport.Client.Core.Extensions;

public static class StringExtensions
{
    public static bool IsEmpty(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static bool IsNotEmpty(this string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public static string ToTitleCase(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return value;
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(value.ToLower());
    }

    public static bool ContainsIgnoreCase(this string source, string value)
    {
        return source?.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    public static bool IsNumeric(this string value)
    {
        return double.TryParse(value, out _);
    }

    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }

    public static string ReverseString(this string value)
    {
        if (string.IsNullOrEmpty(value)) return value;
        char[] charArray = value.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static bool IsEmail(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(value, emailPattern);
    }

    public static string RemoveAllspace(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? value : string.Concat(value.Where(c => !char.IsWhiteSpace(c)));
    }

    /// <summary>
    /// Create Mask For Any String
    /// </summary>
    /// <param name="value">String To Mask</param>
    /// <param name="start">Sart Mask Index</param>
    /// <param name="length">Mask Length</param>
    /// <param name="maskChar">Mask Character</param>
    /// <returns>Example : 1234567890 => 123****7890 </returns>
    public static string Mask(this string value, int start, int length, char maskChar = '*')
    {
        if (string.IsNullOrEmpty(value) || value.Length < start + length) return value;
        var maskedSection = new string(maskChar, length);
        return string.Concat(value.AsSpan(0, start), maskedSection, value.AsSpan(start + length));
    }

    public static string ToCamelCase(this string value)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return char.ToLowerInvariant(value[0]) + value[1..];
    }

    public static string ToPascalCase(this string value)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return char.ToUpperInvariant(value[0]) + value[1..];
    }

    /// <summary>
    /// Repeat String
    /// </summary>
    /// <param name="value">String To Repeat</param>
    /// <param name="count">Repeat Count</param>
    /// <returns>Example : Value=abc, Count=3 => abcabcabc</returns>
    public static string Repeat(this string value, int count)
    {
        if (string.IsNullOrEmpty(value) || count <= 0) return string.Empty;
        return string.Concat(Enumerable.Repeat(value, count));
    }

    /// <summary>
    /// Remove Digits From String
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Example: input:123abc456 => output:123456</returns>
    public static string RemoveDigits(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? value : string.Concat(value.Where(c => !char.IsDigit(c)));
    }

    public static string Left(this string value, int length)
    {
        if (string.IsNullOrEmpty(value) || length <= 0) return string.Empty;
        return value.Length <= length ? value : value[..length];
    }

    public static string Right(this string value, int length)
    {
        if (string.IsNullOrEmpty(value) || length <= 0) return string.Empty;
        return value.Length <= length ? value : value[^length..];
    }

    public static string RemoveSpecialCharacters(this string value)
    {
        return string.IsNullOrEmpty(value) ? value : string.Concat(value.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)));
    }

    /// <summary>
    /// Check If String Contains Any Substring
    /// </summary>
    /// <param name="value"></param>
    /// <param name="substrings"></param>
    /// <returns></returns>
    public static bool ContainsAny(this string value, params string[] substrings)
    {
        if (string.IsNullOrEmpty(value) || substrings == null || substrings.Length == 0) return false;
        return substrings.Any(substring => value.Contains(substring, StringComparison.OrdinalIgnoreCase));
    }

    public static bool IsPalindrome(this string value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        var reversed = new string(value.ToCharArray().Reverse().ToArray());
        return value.Equals(reversed, StringComparison.OrdinalIgnoreCase);
    }

    public static string EnsureStartsWith(this string value, string prefix)
    {
        if (string.IsNullOrEmpty(value)) return prefix;
        return value.StartsWith(prefix) ? value : prefix + value;
    }

    public static string EnsureEndsWith(this string value, string suffix)
    {
        if (string.IsNullOrEmpty(value)) return suffix;
        return value.EndsWith(suffix) ? value : value + suffix;
    }

    public static int CountOccurrences(this string value, string substring)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(substring)) return 0;
        return (value.Length - value.Replace(substring, string.Empty).Length) / substring.Length;
    }

    public static string RemoveHtmlTags(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? value : Regex.Replace(value, "<.*?>", string.Empty);
    }

    public static string RemoveAccents(this string value)
    {
        if (string.IsNullOrEmpty(value)) return value;
        var normalizedString = value.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();
        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    public static string Slugify(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return value;
        value = value.ToLower().RemoveAccents();
        return Regex.Replace(value, @"[^a-z0-9\s-]", string.Empty)
                    .Trim()
                    .Replace(" ", "-")
                    .Replace("--", "-");
    }

    public static string ExtractDigits(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? value : new string(value.Where(char.IsDigit).ToArray());
    }

    public static string Randomize(this string value)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return new string(value.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
    }

    public static string MinimizeString(this string input, int maxLength)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return input.Length > maxLength ? input[..maxLength] : input;
    }
}
