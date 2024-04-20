using WenJiuDiary.Library.Models;

namespace WenJiuDiary.Library.Services;

public class HomeImageStorage : IHomeImageStorage
{

    public static readonly string FullStartDateKey = nameof(HomeImageStorage) +
        "." + nameof(HomeImage.FullStartDate);

    public static readonly string ExpiresAtKey =
        nameof(HomeImageStorage) + "." + nameof(HomeImage.ExpiresAt);

    public static readonly string CopyrightKey =
        nameof(HomeImageStorage) + "." + nameof(HomeImage.Copyright);

    public static readonly string CopyrightLinkKey = nameof(HomeImageStorage) +
        "." + nameof(HomeImage.CopyrightLink);

    public const string FullStartDateDefault = "201901010700";

    public static readonly DateTime ExpiresAtDefault = new(2019, 1, 2, 7, 0, 0);

    public const string CopyrightDefault =
        "Salt field province vietnam work (© Quangpraha/Pixabay)";

    public const string CopyrightLinkDefault =
        "https://pixabay.com/photos/salt-field-province-vietnam-work-3344508/";

    public const string FileName = "homeImage.bin";

    public static readonly string HomeImagePath =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder
                .LocalApplicationData), FileName);

public async Task<HomeImage> GetHomeImageAsync(bool includingImageStream)
{
    var homeImage = new HomeImage
    {

    };
        if (!File.Exists(HomeImagePath))
        {
            await using var imageAssetFileStream =
                new FileStream(HomeImagePath, FileMode.Create) ??
                throw new NullReferenceException("Null file stream.");
            await using var imageAssetStream =
                typeof(HomeImageStorage).Assembly.GetManifestResourceStream(
                    FileName) ??
                throw new NullReferenceException(
                    "Null manifest resource stream");
            await imageAssetStream.CopyToAsync(imageAssetFileStream);
        }

        if (!includingImageStream)
        {
            return homeImage;
        }

        var imageMemoryStream = new MemoryStream();
        await using var imageFileStream =
            new FileStream(HomeImagePath, FileMode.Open);
        await imageFileStream.CopyToAsync(imageMemoryStream);
        homeImage.ImageBytes = imageMemoryStream.ToArray();

        return homeImage;
    }

    public async Task SaveHomeImageAsync(HomeImage homeImage, bool savingExpiresAtOnly)
    {
        if (savingExpiresAtOnly)
        {
            return;
        }

        if (homeImage.ImageBytes == null)
        {
            throw new ArgumentException($"Null image bytes.",
                nameof(homeImage));
        }

        await using var imageFileStream =
            new FileStream(HomeImagePath, FileMode.Create);
        await imageFileStream.WriteAsync(homeImage.ImageBytes, 0,
            homeImage.ImageBytes.Length);
    }

}




