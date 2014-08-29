using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Extensions;
using Nop.Web.Models.CustomGoogleAnalytics;

namespace Nop.Web.Controllers
{
    /// <summary>
    /// Added By Sang Dao
    /// Google Analytics Controller
    /// </summary>
    public partial class CustomGoogleAnalyticsController : BaseNopController
    {
        public ActionResult Index()
        {
            //analytics model
            var model = new AnalyticsResultModel();

            // Get profile id from web.config app settings
            var profileId = ConfigurationManager.AppSettings["ProfileId"];
            
            // Get email address from web.config app settings
            var emailAddress = ConfigurationManager.AppSettings["EmailAddress"];

            // Check if profile id or email address is empty
            if (string.IsNullOrEmpty(profileId) || string.IsNullOrEmpty(emailAddress))
                return Content(""); //return empty

            // Connect to google API
            // Create Google Service Account, then generate p12 file, copy the file to App_Data and change file name here
            // Refer this: https://developers.google.com/drive/web/service-accounts
            var ga = new GoogleConnector(Server.MapPath("~/App_Data/bce605e93b6b.p12"), emailAddress);

            // Get data

            //var allprofiles = ga.Service.Management.Profiles.List("~all", "~all").Execute();
            //// If profile cannot be found
            //if (allprofiles.Items[0] == null) return Content(""); // return empty

            #region Real-time
            // Statistic by Realtime
            var startDate = DateTime.Now.Beginning();
            var endDate = DateTime.Now;

            // Request data
            var byRealtime = ga.GetAnalyticsData("ga:" + profileId, new string[] { "ga:users" },
                startDate, endDate, new string[] { "ga:browser" });

            // Add data to model
            foreach (var browser in byRealtime.Rows)
            {
                model.RealtimeBrowsers.Add(new AnalyticsResultModel.Browser()
                {
                    Name = browser[0],
                    Visit = int.Parse(browser[1])
                });
            }
            model.RealTimeCount = model.RealtimeBrowsers.Sum(t => t.Visit);
            model.RealTimeStartDate = startDate;
            model.RealTimeEndDate = endDate;
            #endregion

            #region By Week
            // Statistic by current Week
            var currentWeekStartDate = DateTime.Now.StartOfWeek();
            var currentWeekEndDate = currentWeekStartDate.AddDays(6);

            // Request Data
            var byThisWeek = ga.GetAnalyticsData(profileId, new string[] { "ga:users" },
                currentWeekStartDate, currentWeekEndDate, new string[] { "ga:browser" });

            // Add data to model
            foreach (var browser in byThisWeek.Rows)
            {
                model.ThisWeekCountBrowsers.Add(new AnalyticsResultModel.Browser()
                {
                    Name = browser[0],
                    Visit = int.Parse(browser[1])
                });
            }
            model.ThisWeekCount = model.ThisWeekCountBrowsers.Sum(t => t.Visit);
            model.ThisWeekStartDate = currentWeekStartDate;
            model.ThisWeekEndDate = currentWeekEndDate;

            // Statistic by last Week
            var lastWeekStartDate = DateTime.Now.StartOfPreviousWeek();
            var lastWeekEndDate = lastWeekStartDate.AddDays(6);

            // Request Data
            var byLastWeek = ga.GetAnalyticsData(profileId, new string[] { "ga:users" },
                lastWeekStartDate, lastWeekEndDate, new string[] { "ga:browser" });

            // Add data to model
            foreach (var browser in byLastWeek.Rows)
            {
                model.LastWeekCountBrowsers.Add(new AnalyticsResultModel.Browser()
                {
                    Name = browser[0],
                    Visit = int.Parse(browser[1])
                });
            }
            model.LastWeekCount = model.LastWeekCountBrowsers.Sum(t => t.Visit);
            model.LastWeekStartDate = lastWeekStartDate;
            model.LastWeekEndDate = lastWeekEndDate;
            #endregion

            #region By Month
            // Statistic by current Month
            var currentMonthStartDate = DateTime.Now.StartOfMonth();
            var currentMonthEndDate = DateTime.Now.EndOfMonth();

            // Request Data
            var byThisMonth = ga.GetAnalyticsData(profileId, new string[] { "ga:users" },
                currentMonthStartDate, currentMonthEndDate, new string[] { "ga:browser" });

            // Add data to model
            foreach (var browser in byThisMonth.Rows)
            {
                model.ThisMonthCountBrowsers.Add(new AnalyticsResultModel.Browser()
                {
                    Name = browser[0],
                    Visit = int.Parse(browser[1])
                });
            }
            model.ThisMonthCount = model.ThisMonthCountBrowsers.Sum(t => t.Visit);
            model.ThisMonthStartDate = currentMonthStartDate;
            model.ThisMonthEndDate = currentMonthEndDate;

            // Statistic by last Month
            var lastMonthStartDate = DateTime.Now.StartOfPreviousMonth();
            var lastMonthEndDate = lastMonthStartDate.EndOfMonth();

            // Request Data
            var byLastMonth = ga.GetAnalyticsData(profileId, new string[] { "ga:users" },
                lastMonthStartDate, lastMonthEndDate, new string[] { "ga:browser" });

            // Add data to model
            foreach (var browser in byLastMonth.Rows)
            {
                model.LastMonthCountBrowsers.Add(new AnalyticsResultModel.Browser()
                {
                    Name = browser[0],
                    Visit = int.Parse(browser[1])
                });
            }
            model.LastMonthCount = model.LastMonthCountBrowsers.Sum(t => t.Visit);
            model.LastMonthStartDate = lastMonthStartDate;
            model.LastMonthEndDate = lastMonthEndDate;
            #endregion

            return PartialView("_Statistic", model);
        }
    }

    /// <summary>
    /// Added By Sang Dao
    /// DateTime Extensions
    /// </summary>
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt)
        {
            int dayOfWeekNumber = (int)DateTime.Today.DayOfWeek - (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

            var previosWeekFirstDay = DateTime.Today.AddDays(- dayOfWeekNumber);
            return previosWeekFirstDay;
        }

        public static DateTime StartOfPreviousWeek(this DateTime dt)
        {
            int dayOfWeekNumber = (int)DateTime.Today.DayOfWeek - (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

            var previosWeekFirstDay = DateTime.Today.AddDays(-7 - dayOfWeekNumber);
            return previosWeekFirstDay;
        }

        public static DateTime StartOfMonth(this DateTime dt)
        {
            var today = dt;
            var firstDay = today.AddDays(-(today.Day - 1));
            return firstDay;
        }

        public static DateTime EndOfMonth(this DateTime dt)
        {
            var today = dt;
            today = today.AddMonths(1);
            var lastDay = today.AddDays(-(today.Day));
            return lastDay;
        }

        public static DateTime StartOfPreviousMonth(this DateTime dt)
        {
            var previosMonthFirstDay = dt.AddMonths(-1);
            previosMonthFirstDay = previosMonthFirstDay.AddDays(-previosMonthFirstDay.Day + 1);
            return previosMonthFirstDay;
        }

        public static DateTime Beginning(this DateTime dt)
        {
            return DateTime.Parse("1/1/2006");
        }

    }
}