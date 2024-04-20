using System.Linq.Expressions;
using WenJiuDiary.Library.Models;
using WenJiuDiary.Library.Services;

namespace WenJiuDiary.Library.Pages;

public partial class DiaryShow
{

    private string _query;

    private int id;

    private Expression<Func<Diary, bool>> _where = p => true;

    public const string Loading = "稍等稍等~";

    public const string NoResult = "你确定你没搜错嘛";

    public const string NoMoreResult = "到头咯~";

    private string _status = string.Empty;

    public const int PageSize = 5;

    private Diary _diary = new();

    private List<Diary> _diaries = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        await LoadMoreAsync();
    }

    private async Task LoadMoreAsync(){
        _status = Loading;
        var diaries =
            await _diaryStorage.GetAllDiariesAsync(_where, _diaries.Count, PageSize);
        _status = string.Empty;
        _diaries.AddRange(diaries);

        
 
        if (diaries.Count() < PageSize)
        {
            _status= NoMoreResult;
        }

        if(diaries.Count() == 0 && _diaries.Count == 0)
        {
            _status= NoResult;
        }
    }


    private async Task QueryAsync()
    {
       var _diaries = await _diaryStorage.SearchAsync(_query, _query);
    }

    private void Diarydelete(int id)
    {
         _diaryStorage.DeleteDiaryAsync(id);
    }


    private void OnClick(Diary diary) =>
        _navigationService.NavigateTo(
            $"{NavigationServiceContants.DetailPage}/{diary.Id}");

}
