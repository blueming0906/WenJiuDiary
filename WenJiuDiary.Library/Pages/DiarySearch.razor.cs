using WenJiuDiary.Library.Services;
using System.Linq.Expressions;
using WenJiuDiary.Library.Models;
using Microsoft.Extensions.Logging;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WenJiuDiary.Library.Pages;



public partial class DiarySearch
{
    private string _name;

    private string _content;
    
    private string _query;

    private ConsoleLogger? Logger { get; set; }


    //暂未完整实现
    private async Task OnEnterAsync(string val)
    {
        _diaries = await _diaryStorage.SearchAsync(_query, _query);
    }


    private async Task QueryAsync()
    {
       _diaries = await _diaryStorage.SearchAsync(_query, _query);
    }

    private IEnumerable<Models.Diary> _diaries
        = new List<Models.Diary>();

    private void OnClick(Diary diary) =>
        _navigationService.NavigateTo(
            $"{NavigationServiceContants.DetailPage}/{diary.Id}");

}