using WenJiuDiary.Library.Models;

namespace WenJiuDiary.Library.Services;

public interface IHomeImageService
{
    Task<HomeImage> GetHomeImageAsync();

    Task<HomeImageServiceCheckUpdateResult> CheckUpdateAsync();
}

public class HomeImageServiceCheckUpdateResult
{
    public bool HasUpdate { get; set; }

    public HomeImage HomeImage { get; set; } = new();
}