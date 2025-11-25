using TechnicalSupport.Client.Components;

namespace TechnicalSupport.Client.Core.Services.Dashboard
{
    public interface IDashboardNotificationService
    {
        Task ShowCompanyUpdateNotificationAsync(Notification notificationRef, string message, int companiesCount);
        Task ShowUserUpdateNotificationAsync(Notification notificationRef, I18nText.MyText myText, string username);
        Task ShowCompanyUserUpdateNotificationAsync(Notification notificationRef, I18nText.MyText myText, string companyUser);
        Task ShowServerAddressUpdateNotificationAsync(Notification notificationRef, I18nText.MyText myText, string serverAddress);
        Task ShowErrorNotificationAsync(Notification notificationRef, string title, string message);
    }

    public class DashboardNotificationService : IDashboardNotificationService
    {
        private I18nText.MyText myText = new();
        public async Task ShowCompanyUpdateNotificationAsync(Notification notificationRef,string message, int companiesCount)
        {
            if (notificationRef != null)
            {
                notificationRef.ShowNotifications(
                    message,
                    $"Count {companiesCount}",
                    NotificationSeverity.Success);
            }
            await Task.CompletedTask;
        }

        public async Task ShowUserUpdateNotificationAsync(Notification notificationRef, I18nText.MyText myText, string username)
        {
            if (notificationRef != null)
            {
                notificationRef.ShowNotifications(
                    myText.userUpdated,
                    myText.userListUpdated + " " + username,
                    NotificationSeverity.Success);
            }
            await Task.CompletedTask;
        }

        public async Task ShowCompanyUserUpdateNotificationAsync(Notification notificationRef, I18nText.MyText myText, string companyUser)
        {
            if (notificationRef != null)
            {
                notificationRef.ShowNotifications(
                    myText.companyUserUpdated,
                    myText.companyUserListUpdated + " " + companyUser,
                    NotificationSeverity.Success);
            }
            await Task.CompletedTask;
        }

        public async Task ShowServerAddressUpdateNotificationAsync(Notification notificationRef, I18nText.MyText myText, string serverAddress)
        {
            if (notificationRef != null)
            {
                notificationRef.ShowNotifications(
                    myText.serverAddressUpdated,
                    myText.serverAddressListUpdated + " " + serverAddress,
                    NotificationSeverity.Success);
            }
            await Task.CompletedTask;
        }

        public async Task ShowErrorNotificationAsync(Notification notificationRef, string title, string message)
        {
            if (notificationRef != null)
            {
                notificationRef.ShowNotifications(title, message, NotificationSeverity.Error);
            }
            await Task.CompletedTask;
        }
    }
}