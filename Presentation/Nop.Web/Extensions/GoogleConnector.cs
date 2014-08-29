using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Analytics.v3;
using Google.Apis.Analytics.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace Nop.Web.Extensions
{
    /// <summary>
    /// Added By Sang Dao
    /// Google Connector Service
    /// </summary>
    public class GoogleConnector
    {
        public AnalyticsService Service { get; set; }

        public GoogleConnector(string keyPath, string accountEmailAddress)
        {
            var certificate = new X509Certificate2(keyPath, "notasecret", X509KeyStorageFlags.Exportable);

            var credentials = new ServiceAccountCredential(
               new ServiceAccountCredential.Initializer(accountEmailAddress)
               {
                   Scopes = new[] { AnalyticsService.Scope.AnalyticsReadonly }
               }.FromCertificate(certificate));

            Service = new AnalyticsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credentials,
                ApplicationName = "Google Analytics Application"
            });
        }

        public AnalyticDataPoint GetAnalyticsData(string profileId, string[] metrics, DateTime startDate, DateTime endDate, string[] dimensions)
        {
            AnalyticDataPoint data = new AnalyticDataPoint();
            if (!profileId.Contains("ga:"))
                profileId = string.Format("ga:{0}", profileId);

            GaData response = null;
            do
            {
                int startIndex = 1;
                if (response != null && !string.IsNullOrEmpty(response.NextLink))
                {
                    Uri uri = new Uri(response.NextLink);
                    var paramerters = uri.Query.Split('&');
                    string s = paramerters.First(i => i.Contains("start-index")).Split('=')[1];
                    startIndex = int.Parse(s);
                }

                var request = BuildAnalyticRequest(profileId, metrics, startDate, endDate, startIndex, dimensions);
                response = request.Execute();
                data.ColumnHeaders = response.ColumnHeaders;
                data.Rows.AddRange(response.Rows);

            }
            while (!string.IsNullOrEmpty(response.NextLink));

            return data;
        }

        private DataResource.GaResource.GetRequest BuildAnalyticRequest(string profileId, string[] metrics,
                                                                            DateTime startDate, DateTime endDate, int startIndex, string[] dimensions)
        {
            DataResource.GaResource.GetRequest request = Service.Data.Ga.Get(profileId, startDate.ToString("yyyy-MM-dd"),
                                                                                endDate.ToString("yyyy-MM-dd"), string.Join(",", metrics));
            request.StartIndex = startIndex;
            if (dimensions != null && dimensions.Any())
                request.Dimensions = string.Join(",", dimensions);
            return request;
        }

        public class AnalyticDataPoint
        {
            public AnalyticDataPoint()
            {
                Rows = new List<IList<string>>();
            }

            public IList<GaData.ColumnHeadersData> ColumnHeaders { get; set; }
            public List<IList<string>> Rows { get; set; }
        }
    }
}
