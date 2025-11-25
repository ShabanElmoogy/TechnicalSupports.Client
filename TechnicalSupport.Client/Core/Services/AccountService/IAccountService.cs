namespace TechnicalSupport.Client.Core.Services.AccountService;

public interface IAccountService
{
    Task<UserProfileViewModel> GetUserInfo();
    Task<ApiResponse> UpdateUserInfo(UserProfileViewModel userProfile);
    Task<ApiResponse> ChangePassword(ChangePasswordViewModel changePassword);
}
