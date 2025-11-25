using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace TechnicalSupport.Client.Core.Services.Dashboard
{
    public class DashboardSignalRService : IDashboardSignalRService
    {
        private readonly IConfiguration _configuration;
        private HubConnection? _hubConnection;
        private Func<string, object, Task>? _onDataUpdated;

        public DashboardSignalRService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InitializeAsync(Func<string, object, Task> onDataUpdated)
        {
            _onDataUpdated = onDataUpdated;
            await ConnectToHubAsync();
        }

        private async Task ConnectToHubAsync()
        {
            try
            {
                var apiAddress = _configuration.GetValue<string>(Strings.MainApiAddress);

                _hubConnection = new HubConnectionBuilder()
                    .WithUrl($"{apiAddress}{SignalRRoutes.CompanyHub}")
                    .Build();

                // Subscribe to all update events
                SubscribeToHubEvents();

                await _hubConnection.StartAsync();
                Console.WriteLine("SignalR Hub connected successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to SignalR Hub: {ex.Message}");
            }
        }

        private void SubscribeToHubEvents()
        {
            if (_hubConnection == null) return;

            // Company Updates
            _hubConnection.On<int>(nameof(SignalRMethods.ReceiveCompanyUpdate),
                async (companyCount) =>
                {
                    if (_onDataUpdated != null)
                        await _onDataUpdated("Company", companyCount);
                });

            // User Updates
            _hubConnection.On<int>(nameof(SignalRMethods.ReceiveUserUpdate),
                async (userCount) =>
                {
                    if (_onDataUpdated != null)
                        await _onDataUpdated("User", userCount);
                });

            // Company User Updates
            _hubConnection.On<int>(nameof(SignalRMethods.ReceiveCompanyUserUpdate),
                async (companyUserCount) =>
                {
                    if (_onDataUpdated != null)
                        await _onDataUpdated("CompanyUser", companyUserCount);
                });

            // Server Address Updates
            _hubConnection.On<int>(nameof(SignalRMethods.ReceiveServerAddressUpdate),
                async (serverAddressCount) =>
                {
                    if (_onDataUpdated != null)
                        await _onDataUpdated("ServerAddress", serverAddressCount);
                });
        }

        public async Task<bool> IsConnectedAsync()
        {
            return _hubConnection?.State == HubConnectionState.Connected;
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection != null)
            {
                try
                {
                    await _hubConnection.StopAsync();
                    await _hubConnection.DisposeAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error disposing SignalR hub: {ex.Message}");
                }
            }
        }
    }
}