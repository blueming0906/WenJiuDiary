using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using WenJiuDiary.Library.Models;
using System.Linq.Expressions;
using System.Net.NetworkInformation;

namespace WenJiuDiary.Library.Pages
{
    public partial class DiaryHistory
    {
        private Expression<Func<Diary, bool>> _where = p => true;

        public const int PageSize = 50;

        public Diary _diary = new();

        private List<Diary> _diaries = new();

        private List<TimelineItem> timelineItems = new List<TimelineItem>();



        private ConsoleLogger? NormalLogger { get; set; }

        private void OnValueChanged(DateTime ts)
        {
            NormalLogger.Log($"{ts:yyyy-MM-dd}");
        }

        private DateTime BindValue { get; set; } = DateTime.Today;

        public string GetColorForDate(DateTime datetime)
        {
            // 根据datetime生成一个颜色值，这里简单起见使用年份的最后一位数字作为颜色索引  
            int colorIndex = datetime.Day % 10;
            string[] colors = { "red", "blue", "green", "orange", "purple", "pink", "brown", "gray", "yellow", "cyan" };
            return colors[colorIndex];
        }

        protected override async Task OnInitializedAsync()
        {
            // 在组件初始化时获取日记数据  
            await GetAllDiary();
        }

        private async Task GetAllDiary()
        {
            var diaries =
                await _diaryStorage.GetAllDiariesAsync(_where, _diaries.Count, PageSize);
            _diaries.AddRange(diaries);

            var timelineItemsToAdd = diaries.Select(diary => new TimelineItem
            {
                Content = diary.Name, 
                Description = diary.CreaTime.ToString("yyyy-MM-dd")
               /* Color = Color.GetColorForDate(diary.CreaTime)*/
            }).ToList();

            timelineItemsToAdd.Sort((x, y) => x.Description.CompareTo(y.Description));

            timelineItems.Clear();

            timelineItems.AddRange(timelineItemsToAdd);

            StateHasChanged();

        }


        //查询组件是否可以采用add方式 获取动态数据

        private TimelineItem[] TimelineItems =
        [
            new ()
            {
/*              Description = _diary.CreaTime.ToString("yyyy-MM-dd"),
                Content = _diary.Name,
                Color = GetColorForDate(_diary.CreaTime)*/
            }
        ];
 


        private readonly TimelineItem[] AlertTimelineItems =
        [
            new()
            {
                Content = "Create a services site 2015-09-01",
            },
            new()
            {
                Color = Color.Success,
                Content = "Solve initial network problems 2015-09-01",
            },
            new()
            {
                Color = Color.Danger,
                Content = "Create a services site 2015-09-01",
            },
            new()
            {
                Color = Color.Warning,
                Content = "Network problems being solved 2015-09-01",
            },
            new()
            {
                Color = Color.Info,
                Content = "Create a services site 2015-09-01",
            }
        ];
    }

    
}
