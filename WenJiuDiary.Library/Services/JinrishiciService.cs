using System.Text.Json;
using Microsoft.Extensions.Options;
using WenJiuDiary.Library.Models;

namespace WenJiuDiary.Library.Services;

public class JinrishiciService : ITodayPoetryService
{
    private readonly IAlertService _alertService;

    private const string Server = "今日诗词服务器";

    public JinrishiciService(IAlertService alertService)
    {
        _alertService = alertService;
    }

    public async Task<TodayPoetry> GetTodayPoetryAsync()
    {
        var token = await GetTokenAsync();
        //TODO 得不到 token

        //http中添加头作为token  爬虫知识
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("X-User-Token", token);

        HttpResponseMessage response;
        try
        {
            response =
                await httpClient.GetAsync("https://v2.jinrishici.com/sentence");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            await _alertService.AlertAsync(ErrorMessages.HttpClientErrorTitle,
                ErrorMessages.GetHttpClientError(Server, e.Message),
                ErrorMessages.HttpClientErrorButton);
            return new TodayPoetry();
            //TODO 得不到诗词
            throw;
        }

        //获取今日诗词数据
        var json = await response.Content.ReadAsStringAsync();
        JinrishiciSentence jinrishiciSentence;
        try
        {
            jinrishiciSentence = JsonSerializer.Deserialize<JinrishiciSentence>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception e)
        {
            await _alertService.AlertAsync(
                ErrorMessages.JsonDeserializationErrorTitle,
                ErrorMessages.GetJsonDeserializationError(Server, e.Message),
                ErrorMessages.JsonDerializationErrorButton);
            return new TodayPoetry();
        }

        var todayPoetry = new TodayPoetry();
        todayPoetry.Snippet = jinrishiciSentence.Data.Content;
        todayPoetry.Name = jinrishiciSentence.Data.Origin.Title;
        todayPoetry.Dynasty = jinrishiciSentence.Data.Origin.Dynasty;
        todayPoetry.Author = jinrishiciSentence.Data.Origin.Author;
        //string join  使用特定符号进行拼接
        todayPoetry.Content = string.Join
            (Environment.NewLine, jinrishiciSentence.Data.Origin.Content);
        todayPoetry.Source = TodayPoetrySources.Local;

        return todayPoetry;
    }

    private string _token = string.Empty;

    public async Task<string> GetTokenAsync()
    {
        //先从存储中读取token 有 返回  没有 获取  未实现 键值存储

        if (!string.IsNullOrWhiteSpace(_token))
        {
            return _token;
        }

        using var httpClient = new HttpClient();

        HttpResponseMessage response;
        try
        {
            response = await httpClient.GetAsync
                ("https://v2.jinrishici.com/token");
            response.EnsureSuccessStatusCode();

        }
        catch (Exception e)
        {
            await _alertService.AlertAsync(ErrorMessages.HttpClientErrorTitle,
                ErrorMessages.GetHttpClientError(Server, e.Message),
                ErrorMessages.HttpClientErrorButton);

            return _token;
        }

        var json = await response.Content.ReadAsStringAsync();
        //json 反序列化
        JinrishiciToken jinrishiciToken;
        try
        {
            jinrishiciToken =
                JsonSerializer.Deserialize<JinrishiciToken>(
                    json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var token = jinrishiciToken.Data;
        }
        catch (Exception e)
        {
            await _alertService.AlertAsync(
                ErrorMessages.JsonDeserializationErrorTitle,
                ErrorMessages.GetJsonDeserializationError(Server, e.Message),
                ErrorMessages.JsonDerializationErrorButton);

            return _token;
        }
        //缓存token到内存中  
        _token = jinrishiciToken.Data;
        //缓存token数据到外存中 需键值存储调用接口  未实现

        return _token;
    }
}

public class JinrishiciToken
{
    public string Status { get; set; }

    public string Data { get; set; }
}


public class JinrishiciSentence
{
    public JinrishiciData Data { get; set; }

}

public class JinrishiciData
{
    public string Content { get; set; }
    public JinrishiciOrigin Origin { get; set; }

}

public class JinrishiciOrigin
{
    public string Title { get; set; }
    public string Dynasty { get; set; }
    public string Author { get; set; }
    public string[] Content { get; set; }
    public object Translate { get; set; }
}
