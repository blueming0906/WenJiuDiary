using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WenJiuDiary.Library.Services;

namespace WenJiuDiary
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //注册软件本地化服务
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("zh-CN")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            CultureInfo.CurrentCulture = new CultureInfo("zh-CN");
            CultureInfo.CurrentUICulture = new CultureInfo("zh-CN");

/*            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            CultureInfo.CurrentUICulture = new CultureInfo("en-US");*/

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBootstrapBlazor();
            builder.Services.AddLocalization();

            //构建app配置代码
            /*            var app = builder.Build();

                        var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
                        app.UseRequestLocalization(localizationOptions);

                        return app;*/


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddScoped<IDiaryStorage,DiaryStorage>();
            builder.Services.AddScoped<INavigationService, NavigationService>();
            builder.Services.AddScoped<IAlertService, AlertService>();
            builder.Services.AddScoped<ITodayPoetryService,JinrishiciService>();
            builder.Services.AddScoped<IParcelBoxService, ParcelBoxService>();

            return builder.Build();
        }
    }
}
