using TechnicalSupport.Client.Core.Services.AuthenticationService;
using static TechnicalSupport.Client.Core.Const.ApiRoutes;

namespace TechnicalSupport.Client.Core.StateManagement.StateContainer;

public class PhotoService
{
    private byte[]? _currentPhoto;

    // Event to notify subscribers when the photo is updated
    public event Action<byte[]>? OnPhotoUpdated;

    private readonly IAuthenticationServiceFactory _authenticationServiceFactory;
    private readonly IAccountService _accountService;

    // Constructor to inject dependencies
    public PhotoService(IAuthenticationServiceFactory authenticationServiceFactory, IAccountService accountService)
    {
        _authenticationServiceFactory = authenticationServiceFactory;
        _accountService = accountService;
    }

    // Method to get the current photo
    public byte[] GetCurrentPhoto() => _currentPhoto;

    // Method to load the photo from the database
    public async Task LoadPhotoFromDatabaseAsync(string userId)
    {
        // Fetch the photo from the database
        var userPhoto = await _authenticationServiceFactory.GetUserPhoto(userId);

        if (userPhoto?.ProfilePicture?.Length > 0 && (_currentPhoto == null || !userPhoto.ProfilePicture.SequenceEqual(_currentPhoto)))
        {
            _currentPhoto = userPhoto.ProfilePicture;

            // Notify subscribers of the updated photo
            OnPhotoUpdated?.Invoke(_currentPhoto);
        }
    }

    // Method to update the photo and sync it with the database
    public async Task UpdatePhotoAsync(UserProfileViewModel user)
    {
        // Update the user info in the database
        await _accountService.UpdateUserInfo(user);

        //// Update the local state
        //_currentPhoto = user.ProfilePicture;

        // Notify all subscribers about the update
        OnPhotoUpdated?.Invoke(_currentPhoto);
    }

    // Method to manually update the current photo and notify subscribers
    public void UpdateCurrentPhoto(byte[] photoData)
    {
        _currentPhoto = photoData;
        OnPhotoUpdated?.Invoke(_currentPhoto);
    }
}
