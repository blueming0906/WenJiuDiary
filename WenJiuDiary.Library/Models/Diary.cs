using SQLite;

namespace WenJiuDiary.Library.Models;

public class Diary{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Content { get; set; }

    public DateTime CreaTime { get; set; } = DateTime.Now;

    //简介
    private string _snippet;

    [SQLite.Ignore]
    public string Snippet =>
        _snippet ??= Content.Split(separator: '。')[0].Replace(oldValue: "\r\n", newValue: "");


}
