using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace WenJiuDiary.Library.Pages;

public partial class DiarySetting
{

    private IEnumerable<SelectedItem>? DemoValues { get; set; }


    protected override void OnInitialized()
    {
        DemoValues = new List<SelectedItem>(2)
        {
            new ("1", "中文"), 
            new ("2", "英语")
        };

    }

}


[Route("api/[controller]")]
public class AboutController : Controller
{
    private readonly IStringLocalizer<AboutController> _localizer;

    public AboutController(IStringLocalizer<AboutController> localizer)
    {
        _localizer = localizer;
    }

    [HttpGet]
    public string Get()
    {
        return _localizer["DarkMode"];
    }
}