using Microsoft.AspNetCore.Components;
using WenJiuDiary.Library.Models;
using WenJiuDiary.Library.Services;

namespace WenJiuDiary.Library.Pages;

public partial class DiaryWrite
{
    private string _name;

    private string _content;

    private async Task SaveAsync()
    {
        var diary = new Models.Diary
        {
            Name = _name,
            Content = _content,
            CreaTime =  DateTime.Now
        };
        await _diaryStorage.SaveDiaryAsync(diary);
        _navigationService.NavigateTo(
            $"{NavigationServiceContants.DiaryPage}");
    }



    [Parameter]
    public int Id { get; set; }

}
