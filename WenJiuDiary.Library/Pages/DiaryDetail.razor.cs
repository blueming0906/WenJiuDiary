using WenJiuDiary.Library.Models;
using WenJiuDiary.Library.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace WenJiuDiary.Library.Pages;

public partial class DiaryDetail
{
    [Parameter]
    public string Id { get; set; }

    public Diary diary = new ();

    private Diary _diary = new ();

    private bool _isLoadingDiary = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(Id))
        {
            return;
        }

        if (!int.TryParse(Id, out var diaryId))
        {
            return;
        }

        _isLoadingDiary = true;
        StateHasChanged();

        _diary = await _diaryStorage.GetDiaryAsync(diaryId) ??
                 new Diary { Id = diaryId };
        _diary.Content = _diaryStorage.HtmlToPlainText(_diary.Content);
        _isLoadingDiary = false;
        StateHasChanged();

    }



    private async Task DeleteDiary()
    {
        if (!int.TryParse(Id, out var diaryId))
        {
            return;
        }
        
        _diaryStorage.DeleteDiaryAsync(diaryId);
        _navigationService.NavigateTo(
            $"{NavigationServiceContants.DiaryPage}");
    }

    private async Task UpdateDiary()
    {
        if (!int.TryParse(Id, out var diaryId))
        {
            return;
        }
        _navigationService.NavigateTo(
            $"{NavigationServiceContants.UpdatePage}/{diaryId}");
    }

    private async Task BackShow()
    {
        _navigationService.NavigateTo(
            $"{NavigationServiceContants.DiaryPage}");
    }

}