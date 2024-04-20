using WenJiuDiary.Library.Models;

namespace WenJiuDiary.Library.Pages;

public partial class Home
{
    private TodayPoetry _todayPoetry = new();

    private bool _isLoading = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _isLoading = true;
        StateHasChanged();

        _todayPoetry = await _TodayPoetryService.GetTodayPoetryAsync();
        _isLoading = false;
        //强制刷新页面
        StateHasChanged();
    }
}