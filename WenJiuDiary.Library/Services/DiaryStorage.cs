using SQLite;
using System.Linq.Expressions;
using WenJiuDiary.Library.Models;
using WenJiuDiary.Library.Pages;
    using System.Text.RegularExpressions;

namespace WenJiuDiary.Library.Services;

public class DiaryStorage : IDiaryStorage
{
    //数据库命名
    public const string DbName = "db";

    public static readonly string DiaryDbPath =
        Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder
                    .LocalApplicationData), DbName);

    //创建连接
    private SQLiteAsyncConnection _connection;

    //打开链接
    private SQLiteAsyncConnection Connection =>
        _connection ??=
            new SQLiteAsyncConnection(DiaryDbPath);


    public async Task InitializeAsync() =>
        await Connection.CreateTableAsync<Diary>();




    public async Task SaveDiaryAsync(Diary diary) =>
        await Connection.InsertAsync(diary);

    public async Task<Diary> GetDiaryAsync(int id) =>
        await Connection.Table<Diary>()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
    //select * from Diary where Id = id limit 1

    //全部显示  循环显示  
    public async Task<IEnumerable<Diary>> GetAllDiariesAsync(
        Expression<Func<Diary, bool>> where, int skip, int take) =>
        await Connection.Table<Diary>().Where(where).Skip(skip).Take(take)
            .ToListAsync();


    public async Task<IEnumerable<Diary>> SearchAsync(string name, string content) =>
        await Connection.Table<Diary>()
            .Where(p => p.Name.Contains(name) || p.Content.Contains(content))                          //Contains  字符串对比  返回true false
            .ToListAsync();

 

    public async Task DeleteDiaryAsync(int id)
    {
        var diaryToDelete = await Connection.Table<Diary>()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (diaryToDelete != null)
        {
            await Connection.DeleteAsync(diaryToDelete);
        }
    }

    public async Task UpdateDiaryAsync(Diary diary)
    {
        // 确保传入的Diary对象有有效的Id  
        if (diary == null || diary.Id == 0)
        {
            throw new ArgumentException("Diary to update must have a valid Id.");
        }

        // 从数据库中获取要更新的原始Diary对象  
        var originalDiary = await Connection.Table<Diary>()
            .Where(p => p.Id == diary.Id)
            .FirstOrDefaultAsync();

        if (originalDiary != null)
        {
            // 更新原始Diary对象的属性  
            originalDiary.Name = diary.Name;
              
            originalDiary.Content = diary.Content;

            originalDiary.CreaTime = diary.CreaTime;

            // 保存更改回数据库  
            await Connection.UpdateAsync(originalDiary);
        }
        else
        {
            throw new InvalidOperationException("Diary with the specified Id was not found.");
        }
    }


  
  
    public string HtmlToPlainText(string html)
    {
        // 正则表达式匹配所有的 HTML 标签  
        var htmlTags = @"<[^>]*>";
        return Regex.Replace(html, htmlTags, string.Empty);
    }
}
