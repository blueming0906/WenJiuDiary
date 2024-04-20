using Microsoft.AspNetCore.Components;

namespace WenJiuDiary.Library.Services;

public class NavigationService : INavigationService
{
    private readonly NavigationManager _navigationManager;
    private readonly IParcelBoxService _parcelBoxService;

    public NavigationService(NavigationManager navigationManager, IParcelBoxService parcelBoxService)
    {
        _navigationManager = navigationManager;
        _parcelBoxService = parcelBoxService;
    }
    public void NavigateTo(string url) => _navigationManager.NavigateTo(url);

    public void NavigateTo(string url, object parameter)
    {
        var token = _parcelBoxService.Put(parameter);
        _navigationManager.NavigateTo($"{url}/{token}");
    }
}
