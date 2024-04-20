﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent.I18n;

namespace WenJiuDiary.Library.Services
{
    public abstract  class I18nService : II18nService
    {

        private readonly I18n _i18n;

        public CultureInfo Culture => _i18n.Culture;

        public abstract Dictionary<string, string> Languages { get; }

        public event Action<CultureInfo>? OnChanged;

        public I18nService(I18n i18n)
        {
            _i18n = i18n;
        }

        public void SetCulture(string culture)
        {
            _i18n.SetCulture(new CultureInfo(culture));
            OnChanged?.Invoke(Culture);
        }

        public string T(string? key) => T(key, true) ?? key ?? string.Empty;

        public string? T(string? key, bool whenNullReturnKey)
        {
            if (key == null)
            {
                return string.Empty;
            }

            if (_i18n is null)
            {
                return string.Empty;
            }

            return _i18n.T(key, whenNullReturnKey);
        }

    }
}
