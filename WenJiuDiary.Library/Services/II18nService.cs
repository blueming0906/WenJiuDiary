using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenJiuDiary.Library.Services
{
    public interface II18nService
    {

        event Action<CultureInfo> OnChanged;

        CultureInfo Culture { get; }

        Dictionary<string, string> Languages { get; }

        string T(string? key);

        string? T(string? key, bool whenNullReturnKey);

        void SetCulture(string culture);

    }
}

