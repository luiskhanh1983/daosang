using System;
using System.Collections.Generic;

namespace Nop.Web.Models.CustomGoogleAnalytics
{
    /// <summary>
    /// Google Analytics Result Model
    /// </summary>
    public class AnalyticsResultModel
    {
        public AnalyticsResultModel()
        {
            RealtimeBrowsers = new List<Browser>();
            LastWeekCountBrowsers = new List<Browser>();
            ThisMonthCountBrowsers = new List<Browser>();
            LastMonthCountBrowsers = new List<Browser>();
            ThisWeekCountBrowsers = new List<Browser>();
        }

        public int RealTimeCount { get; set; }
        public DateTime RealTimeStartDate { get; set; }
        public DateTime RealTimeEndDate { get; set; }
        public List<Browser> RealtimeBrowsers { get; set; }
        public int ThisWeekCount { get; set; }
        public DateTime ThisWeekStartDate { get; set; }
        public DateTime ThisWeekEndDate { get; set; }
        public List<Browser> ThisWeekCountBrowsers { get; set; }
        public int LastWeekCount { get; set; }
        public DateTime LastWeekStartDate { get; set; }
        public DateTime LastWeekEndDate { get; set; }
        public List<Browser> LastWeekCountBrowsers { get; set; }
        public int ThisMonthCount { get; set; }
        public DateTime ThisMonthStartDate { get; set; }
        public DateTime ThisMonthEndDate { get; set; }
        public List<Browser> ThisMonthCountBrowsers { get; set; }
        public int LastMonthCount { get; set; }
        public DateTime LastMonthStartDate { get; set; }
        public DateTime LastMonthEndDate { get; set; }
        public List<Browser> LastMonthCountBrowsers { get; set; }

        public class Browser
        {
            public string Name { get; set; }
            public int Visit { get; set; }
        }
    }
}