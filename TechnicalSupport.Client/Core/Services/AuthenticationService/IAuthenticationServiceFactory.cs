namespace TechnicalSupport.Client.Core.Services.AuthenticationService;

public interface IAuthenticationServiceFactory
{
    Task<ApiResponse> RegisterUser(RegisterUserRequest userRegistration, string returnUrl);

    Task<ApiResponse> ConfirmEmail(ConfirmEmailRequest request);

    Task<ApiResponse> ResendConfirmEmail(ResendConfirmationEmailRequest request, string returnUrl);

    Task<ApiResponse<LoginResponse>> Login(LoginRequest login);

    Task OpenHangfire();

    Task<ApiResponse> ForgetPassword(ForgorPasswordViewModel forgorPasswordViewModel, string returnUrl);

    Task<ApiResponse> ResetPassword(ResetPasswordRequest resetPasswordRequest);

    Task<UserPhoto> GetUserPhoto(string userId);

    Task LogoutAsync();

    ValueTask<string> GetJwtAsync();

    Task<bool> RefreshAsync();
}
