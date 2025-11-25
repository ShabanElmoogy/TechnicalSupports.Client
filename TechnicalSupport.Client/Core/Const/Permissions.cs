namespace TechnicalSupport.Client.Core.Const;

public static class Permissions
{
    public static string Type { get; } = "Permissions";

    public const string ViewAccounts = "Accounts:View";
    public const string CreateAccounts = "Accounts:Create";
    public const string EditAccounts = "Accounts:Edit";
    public const string DeleteAccounts = "Accounts:Delete";

    public const string ViewAdministrations = "Administrations:View";
    public const string CreateAdministrations = "Administrations:Create";
    public const string EditAdministrations = "Administrations:Edit";
    public const string DeleteAdministrations = "Administrations:Delete";

    public const string ViewUsers = "Users:View";
    public const string CreateUsers = "Users:Create";
    public const string EditUsers = "Users:Edit";
    public const string DeleteUsers = "Users:Delete";

    public const string ViewRoles = "Roles:View";
    public const string CreateRoles = "Roles:Create";
    public const string EditRoles = "Roles:Edit";
    public const string DeleteRoles = "Roles:Delete";

    public const string ViewCompanies = "Companies:View";
    public const string CreateCompanies = "Companies:Create";
    public const string EditCompanies = "Companies:Edit";
    public const string DeleteCompanies = "Companies:Delete";

    public const string ViewCompanyUsers = "CompanyUsers:View";
    public const string CreateCompanyUsers = "CompanyUsers:Create";
    public const string EditCompanyUsers = "CompanyUsers:Edit";
    public const string DeleteCompanyUsers = "CompanyUsers:Delete";

    public const string ViewServers = "ServersAddress:View";
    public const string CreateServers = "ServersAddress:Create";
    public const string EditServers = "ServersAddress:Edit";
    public const string DeleteServers = "ServersAddress:Delete";

    public const string ViewFiles = "Files:View";
    public const string CreateFiles = "Files:Create";
    public const string EditFiles = "Files:Edit";
    public const string DeleteFiles = "Files:Delete";

    public static IList<string?> GetAllPermissions() =>
     typeof(Permissions).GetFields().Select(x => x.GetValue(x) as string).ToList();
}