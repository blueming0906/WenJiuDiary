using Microsoft.AspNetCore.Components;
using WenJiuDiary.Library.Models;
using WenJiuDiary.Library.Services;

namespace WenJiuDiary.Library.Pages;

public partial class DiaryUpdate
{
    [Parameter]
    public string Id { get; set; }

    private string _name;

    private string _content;

    public Diary  _diary1= new();

    private Diary _diary = new();



    private bool _IsLoadingDiary = true;

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

        _IsLoadingDiary = true;
        StateHasChanged();

        _diary = await _diaryStorage.GetDiaryAsync(diaryId) ??
                 new Diary { Id = diaryId };
        _IsLoadingDiary = false;

        StateHasChanged();



    }

    private async Task UpdateAsync()
    {
        if (!int.TryParse(Id, out var diaryId))
        {
            return;
        }
        _diary1 = await _diaryStorage.GetDiaryAsync(diaryId) ??
                 new Diary { Id = diaryId };

        var diary = new Models.Diary
        {
            Id = _diary1.Id,
            Name = _diary.Name,
            Content = _diary.Content,
            CreaTime = DateTime.Now
        };

          await _diaryStorage.UpdateDiaryAsync(diary);
        _navigationService.NavigateTo(
            $"{NavigationServiceContants.DiaryPage}");
    }
}