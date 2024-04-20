using System.Linq.Expressions;
using WenJiuDiary.Library.Models;
using WenJiuDiary.Library.Pages;

namespace WenJiuDiary.Library.Services;

public interface IDiaryStorage
{
    //读写编辑
    Task InitializeAsync();

    Task SaveDiaryAsync(Diary diary);

    Task DeleteDiaryAsync(int id);

    Task UpdateDiaryAsync(Diary diary);

    Task<Diary> GetDiaryAsync(int id);

    Task<IEnumerable<Diary>> GetAllDiariesAsync(
        Expression<Func<Diary, bool>> where, int skip, int take);

    Task<IEnumerable<Diary>> SearchAsync(string name, string content);

    string HtmlToPlainText(string html);

}
