using TechnicalSupport.Client.ViewModels.Dashboard;

namespace TechnicalSupport.Client.Core.Services.Dashboard
{
    public interface IDashboardDataService
    {
        Task<DashboardData> LoadAllDashboardDataAsync();
        Task<CompanyRelatedData> LoadCompanyRelatedDataAsync();
        Task<CompanyUserRelatedData> LoadCompanyUserRelatedDataAsync();
        Task<ServerRelatedData> LoadServerRelatedDataAsync();
        Task<List<UserViewModel>> LoadUsersAsync();
        Task<int> LoadCompaniesCountAsync();
    }

    public class DashboardData
    {
        public List<CompanyViewModel> Companies { get; set; } = [];
        public List<UserViewModel> Users { get; set; } = [];
        public List<CompanyUsersViewModel> CompanyUsers { get; set; } = [];
        public List<ServersAddressViewModel> ServersAddress { get; set; } = [];
        public List<CompanyWithUserCount> CompaniesWithUserCounts { get; set; } = [];
        public List<ServersWithCompanyCount> ServersWithCompaniesCount { get; set; } = [];
        public int CompaniesCount { get; set; }
    }

    public class CompanyRelatedData
    {
        public List<CompanyViewModel> Companies { get; set; } = [];
        public List<CompanyWithUserCount> CompaniesWithUserCounts { get; set; } = [];
        public List<ServersWithCompanyCount> ServersWithCompaniesCount { get; set; } = [];
    }

    public class CompanyUserRelatedData
    {
        public List<CompanyUsersViewModel> CompanyUsers { get; set; } = [];
        public List<CompanyWithUserCount> CompaniesWithUserCounts { get; set; } = [];
    }

    public class ServerRelatedData
    {
        public List<ServersAddressViewModel> ServersAddress { get; set; } = [];
        public List<ServersWithCompanyCount> ServersWithCompaniesCount { get; set; } = [];
    }
}