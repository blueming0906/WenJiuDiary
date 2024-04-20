namespace WenJiuDiary.Library.Services;

public interface INavigationService{
    void NavigateTo(string url);

    void NavigateTo(string url, object parameter);
}

public static class NavigationServiceContants
{
    public const string DetailPage = "/DiaryDetail";

    public const string HomePage = "/Home";

    public const string DiaryPage = "/DiaryShow";

    public const string InitializationPage = "/Initialization";

    public const string UpdatePage = "/DiaryUpdate";
}