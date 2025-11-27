using TechnicalSupport.Client.ViewModels.Dashboard;

namespace TechnicalSupport.Client.Core.Services.Dashboard
{
    public class DashboardDataService : IDashboardDataService
    {
        private readonly IMainService<CompanyViewModel> _companyService;
        private readonly IMainService<UserViewModel> _userService;
        private readonly IMainService<CompanyUsersViewModel> _companyUsersService;
        private readonly IMainService<ServersAddressViewModel> _serverAddressService;
        private readonly IMainService<EntityCountResponse> _dashboardService;
        private readonly IMainService<CompanyWithUserCount> _companyWithUserCountService;
        private readonly IMainService<ServersWithCompanyCount> _serversWithCompanyCountService;

        public DashboardDataService(
            IMainService<CompanyViewModel> companyService,
            IMainService<UserViewModel> userService,
            IMainService<CompanyUsersViewModel> companyUsersService,
            IMainService<ServersAddressViewModel> serverAddressService,
            IMainService<EntityCountResponse> dashboardService,
            IMainService<CompanyWithUserCount> companyWithUserCountService,
            IMainService<ServersWithCompanyCount> serversWithCompanyCountService)
        {
            _companyService = companyService;
            _userService = userService;
            _companyUsersService = companyUsersService;
            _serverAddressService = serverAddressService;
            _dashboardService = dashboardService;
            _companyWithUserCountService = companyWithUserCountService;
            _serversWithCompanyCountService = serversWithCompanyCountService;
        }

        public async Task<DashboardData> LoadAllDashboardDataAsync()
        {
            try
            {
                // Load data in parallel where possible
                var companiesTask = LoadCompaniesAsync();
                var usersTask = LoadUsersAsync();
                var companyUsersTask = LoadCompanyUsersAsync();
                var serverAddressTask = LoadServerAddressAsync();
                var companiesWithUserCountsTask = LoadCompaniesWithCompanyUsersCountAsync();
                var serversWithCompaniesCountTask = LoadServersWithCompaniesCountAsync();
                var companiesCountTask = LoadCompaniesCountAsync();

                await Task.WhenAll(
                    companiesTask,
                    usersTask,
                    companyUsersTask,
                    serverAddressTask,
                    companiesWithUserCountsTask,
                    serversWithCompaniesCountTask,
                    companiesCountTask
                );

                return new DashboardData
                {
                    Companies = await companiesTask,
                    Users = await usersTask,
                    CompanyUsers = await companyUsersTask,
                    ServersAddress = await serverAddressTask,
                    CompaniesWithUserCounts = await companiesWithUserCountsTask,
                    ServersWithCompaniesCount = await serversWithCompaniesCountTask,
                    CompaniesCount = await companiesCountTask
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dashboard data: {ex.Message}");
                return new DashboardData();
            }
        }

        public async Task<CompanyRelatedData> LoadCompanyRelatedDataAsync()
        {
            try
            {
                var companiesTask = LoadCompaniesAsync();
                var companiesWithUserCountsTask = LoadCompaniesWithCompanyUsersCountAsync();
                var serversWithCompaniesCountTask = LoadServersWithCompaniesCountAsync();

                await Task.WhenAll(companiesTask, companiesWithUserCountsTask, serversWithCompaniesCountTask);

                return new CompanyRelatedData
                {
                    Companies = await companiesTask,
                    CompaniesWithUserCounts = await companiesWithUserCountsTask,
                    ServersWithCompaniesCount = await serversWithCompaniesCountTask
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading company related data: {ex.Message}");
                return new CompanyRelatedData();
            }
        }

        public async Task<CompanyUserRelatedData> LoadCompanyUserRelatedDataAsync()
        {
            try
            {
                var companyUsersTask = LoadCompanyUsersAsync();
                var companiesWithUserCountsTask = LoadCompaniesWithCompanyUsersCountAsync();

                await Task.WhenAll(companyUsersTask, companiesWithUserCountsTask);

                return new CompanyUserRelatedData
                {
                    CompanyUsers = await companyUsersTask,
                    CompaniesWithUserCounts = await companiesWithUserCountsTask
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading company user related data: {ex.Message}");
                return new CompanyUserRelatedData();
            }
        }

        public async Task<ServerRelatedData> LoadServerRelatedDataAsync()
        {
            try
            {
                var serverAddressTask = LoadServerAddressAsync();
                var serversWithCompaniesCountTask = LoadServersWithCompaniesCountAsync();

                await Task.WhenAll(serverAddressTask, serversWithCompaniesCountTask);

                return new ServerRelatedData
                {
                    ServersAddress = await serverAddressTask,
                    ServersWithCompaniesCount = await serversWithCompaniesCountTask
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading server related data: {ex.Message}");
                return new ServerRelatedData();
            }
        }

        public async Task<List<UserViewModel>> LoadUsersAsync()
        {
            try
            {
                var result = await _userService.GetAll($"{ApiRoutes.v1}/{ApiRoutes.Users.GetAll}");
                return result?.OrderBy(f => f.CreatedOn).ToList() ?? new List<UserViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading users: {ex.Message}");
                return new List<UserViewModel>();
            }
        }

        public async Task<int> LoadCompaniesCountAsync()
        {
            try
            {
                var response = await _dashboardService.GetRow($"{ApiRoutes.v1}/{ApiRoutes.Dashboard.GetCompaniesCount}");
                return response?.Count ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading companies count: {ex.Message}");
                return 0;
            }
        }

        private async Task<List<CompanyViewModel>> LoadCompaniesAsync()
        {
            try
            {
                var result = await _companyService.GetAll($"{ApiRoutes.v1}/{ApiRoutes.Companies.GetAllCompanies}");
                return result ?? new List<CompanyViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading companies: {ex.Message}");
                return new List<CompanyViewModel>();
            }
        }

        private async Task<List<CompanyUsersViewModel>> LoadCompanyUsersAsync()
        {
            try
            {
                var result = await _companyUsersService.GetAll($"{ApiRoutes.v1}/{ApiRoutes.CompanyUsers.GetAllCompanyUsers}");
                return result ?? new List<CompanyUsersViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading company users: {ex.Message}");
                return new List<CompanyUsersViewModel>();
            }
        }

        private async Task<List<ServersAddressViewModel>> LoadServerAddressAsync()
        {
            try
            {
                var result = await _serverAddressService.GetAll($"{ApiRoutes.v1}/{ApiRoutes.ServersAddress.GetAllServersAddress}");
                return result ?? new List<ServersAddressViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading server addresses: {ex.Message}");
                return new List<ServersAddressViewModel>();
            }
        }

        private async Task<List<CompanyWithUserCount>> LoadCompaniesWithCompanyUsersCountAsync()
        {
            try
            {
                var response = await _companyWithUserCountService.GetAll($"{ApiRoutes.v1}/{ApiRoutes.Dashboard.GetCompanyWithCompanyUsersCount}");
                return response ?? new List<CompanyWithUserCount>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading companies with user counts: {ex.Message}");
                return new List<CompanyWithUserCount>();
            }
        }

        private async Task<List<ServersWithCompanyCount>> LoadServersWithCompaniesCountAsync()
        {
            try
            {
                var response = await _serversWithCompanyCountService.GetAll($"{ApiRoutes.v1}/{ApiRoutes.Dashboard.GetServersWithCompaniesCount}");
                return response ?? new List<ServersWithCompanyCount>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading servers with company counts: {ex.Message}");
                return new List<ServersWithCompanyCount>();
            }
        }
    }
}