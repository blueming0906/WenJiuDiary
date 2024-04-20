using WenJiuDiary.Library.Models;

namespace WenJiuDiary.Library.Services;

public interface IHomeImageStorage
{
    Task<HomeImage> GetHomeImageAsync(bool includingImageStream);

    Task SaveHomeImageAsync(HomeImage homeImage, bool savingExpiresAtOnly);

}