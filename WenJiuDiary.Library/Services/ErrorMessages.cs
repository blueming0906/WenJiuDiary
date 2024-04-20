namespace WenJiuDiary.Library.Services;

public class ErrorMessages
{
     public const string HttpClientErrorTitle = "连接错误";

     public const string HttpClientErrorButton = "确定";

    public static string GetHttpClientError(string server, string message) =>
         $"与{server}连接时发生了错误：\n{message}";

     public const string JsonDeserializationErrorTitle = "读取数据失败";

     public static string GetJsonDeserializationError(string server, string message)
         => $"从{server}读取数据时发生了错误：\n{message}";

     public const string JsonDerializationErrorButton = "确定";

     public const string PhotoErrorTitle = "照片错误";

     public const string PhotoErrorButton = "确定";


}