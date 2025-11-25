namespace TechnicalSupport.Client.Core.Const;

public static class ApiRoutes
{
    public const string v1 = nameof(v1);

    public const string v2 = nameof(v2);

    public static class Roles
    {
        public const string GetAllRoles = "api/Roles/GetAll";

        public const string AddRole = "api/Roles/Add";

        public const string UpdateRole = "api/Roles/Update";

        public const string ToggleRole = "api/Roles/Toggle";

        public const string GetRoleClaims = "api/Roles/GetRoleClaims";

        public const string UpdateRoleClaims = "api/Roles/UpdateRoleClaims";
    }

    public static class Users
    {
        public const string GetAll = "api/Users/GetAll";

        public const string AddUser = "api/Users/Add";

        public const string UpdateUser = "api/Users/Update";

        public const string ToggleUsers = "api/Users/Toggle";

        public const string GetUserPhoto = "api/Users/GetUserPhoto";
    }

    public static class Export
    {
        public const string ExportExcel = "api/Export/ExportExcel";

        public const string ExportCsv = "api/Export/ExportCsv";

        public const string ExportPdf = "api/ExportPdf/GenerateSyncfusionPdf";
    }

    public static class AccountInfo
    {
        public const string GetUserPhoto = "AccountInfo/GetUserPhoto";

        public const string GetUserInfo = "AccountInfo/GetInfo";

        public const string UpdateUserInfo = "AccountInfo/UpdateInfo";

        public const string ChangePassword = "AccountInfo/ChangePassword";
    }

    public static class Auth
    {
        public const string Register = "api/Auth/Register";

        public const string ConfirmEmail = "api/Auth/ConfirmEmail";

        public const string ResendConfirmationEmailAsync = "api/Auth/ResendConfirmationEmail";

        public const string Login = "api/Auth/Login";

        public const string LogOut = "api/Auth/LogOut";

        public const string ForgetPassword = "api/Auth/ForgetPassword";

        public const string ResetPassword = "api/Auth/ResetPassword";

        public const string RefreshToken = "api/Auth/RefreshToken";

        public const string HangFire = "/jobs";
    }

    public static class Companies
    {
        public const string GetAllCompanies = "api/Companies/GetAll";

        public const string AddCompany = "api/Companies/Add";

        public const string AddCompanies = "api/Companies/AddRange";

        public const string UpdateCompany = "api/Companies/Update";

        public const string DeleteCompany = "api/Companies/Delete";

        public const string DeleteAllCompanies = "api/Companies/DeleteRange";

        public const string PrintCompanies = "api/Companies/GenerateReport/pdf";

        public const string GetCompanyCount = "api/Companies/GetCompanyCount";
    }

    public static class CompanyUsers
    {
        public const string GetAllCompanyUsers = "api/CompanyUsers/GetAll";

        public const string GetByCompanyId = "api/CompanyUsers/GetByCompanyId/company";

        public const string AddCompanyUser = "api/CompanyUsers/Add";

        public const string AddRangeCompanyUsers = "api/CompanyUsers/AddRange";

        public const string UpdateCompanyUser = "api/CompanyUsers/Update";

        public const string DeleteCompanyUser = "api/CompanyUsers/Delete";

        public const string DeleteAllCompanyUsers = "api/CompanyUsers/DeleteRange";
    }

    public static class Dashboard
    {
        public const string GetCompaniesCount = "api/Dashboard/GetCompaniesCount";

        public const string GetCompaniesIdsCount = "api/Dashboard/GetCompaniesIdsCount";

        public const string GetCompanyUsersCount = "api/Dashboard/GetCompanyUsersCount";

        public const string GetServersCount = "api/Dashboard/GetServersCount";

        public const string GetUsersCount = "api/Dashboard/GetUsersCount";

        public const string GetCompanyWithCompanyUsersCount = "api/Dashboard/GetCompanyWithCompanyUsersCount";

        public const string GetServersWithCompaniesCount = "api/Dashboard/GetServersWithCompaniesCount";
    }

    public static class ServersAddress
    {
        public const string GetAllServersAddress = "api/ServersAddress/GetAll";

        public const string GetServersAddressWithCompanies = "api/ServersAddress/GetServerWithCompanies";

        public const string GetAllServersAddressPagination = "api/ServersAddress/GetAllWithPagination";

        public const string AddServerAddress = "api/ServersAddress/Add";

        public const string AddRangeServerAddress = "api/ServersAddress/AddRange";

        public const string UpdateServerAddress = "api/ServersAddress/Update";

        public const string DeleteServerAddress = "api/ServersAddress/Delete";

        public const string DeleteAllServersAddress = "api/ServersAddress/DeleteRange";
    }

    public static class EntityChanges
    {
        public const string GetAllEntityChanges = "api/EntityChangeLogs/GetAllChangesLogs";
    }

    public static class UploadFiles
    {
        public const string GetAllFiles = "api/Files/GetAll";

        public const string UploadFile = "api/Files/Upload";

        public const string UploadManyFiles = "api/Files/UploadMany";

        public const string UploadImage = "api/Files/UploadImage";

        public const string DownLoadImage = "api/Files/Download";

        public const string DownLoadStream = "api/Files/Stream";

        public const string DeleteFile = "api/Files/Delete";

        public const string CheckAuthorization = "api/Files/CheckAuthorization";
    }

    public static class Appointments
    {
        public const string GetAllAppointments = "api/Appointments/GetAll";

        public const string AddAppointment = "api/Appointments/Add";

        public const string EditAppointment = "api/Appointments/Update";
    }
       
}
