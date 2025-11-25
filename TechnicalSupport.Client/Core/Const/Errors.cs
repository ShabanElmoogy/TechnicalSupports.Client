namespace TechnicalSupport.Client.Core.Const;

public static class Errors
{
    public const string RequiredField = "Required field";
    public const string MaxLength = "{0} cannot be more than {1} characters";
    public const string MaxMinLength = "The {0} must be at least {2} and at max {1} characters long.";
    public const string Duplicated = "Another record with the same {0} is already exists!";
    public const string NotAllowFutureDates = "Date cannot be in the future!";
    public const string InvalidRange = "{0} should be between {2} and {1}!";
    public const string ConfirmPasswordNotMatch = "The password and confirmation password do not match.";
    public const string WeakPassword = "Passwords contain an uppercase character, lowercase character, a digit, and a non-alphanumeric character. Passwords must be at least 8 characters long";
    public const string InvalidUsername = "Username can only contain letters.";
    public const string OnlyEnglishLetters = "Only English letters are allowed.";
    public const string OnlyArabicLetters = "Only Arabic letters are allowed.";
    public const string OnlyNumbersAndLetters = "Only Arabic/English letters or digits are allowed.";
    public const string DenySpecialCharacters = "Special characters are not allowed.";
    public const string InvalidMobileNumber = "Invalid mobile number.";
    public const string InvalidNationalId = "Invalid national ID.";
    public const string InvalidStartDate = "Invalid start date.";
    public const string InvalidEndDate = "Invalid end date.";
    public const string InvalidEmail = "Wrong Email Format";
    public const string NoUserFound = "No Users Found";
    public const string NoEmailFound = "No Email Found";
    public const string EmailExists = "Email is Already Exists";
    public const string UserNameExists = "UserName is Already Exists";
    public const string LoginFailed = "Failed To Login";
    public const string AddRoleFailed = "Cannot Add Role";
    public const string NoRoleWithName = "Cannot Find Role With This Name";
}