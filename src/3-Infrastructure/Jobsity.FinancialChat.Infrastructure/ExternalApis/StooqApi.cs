using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Jobsity.FinancialChat.Application.Common.Interfaces.ExternalApis;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using CsvHelper;

namespace Jobsity.FinancialChat.Infrastructure.ExternalApis
{
    public class StooqApi : IStooqApi
    {
        private readonly ILogger<StooqApi> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public StooqApi(ILogger<StooqApi> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetStockQuote(string stockName)
        {
            try
            {
                _logger.LogInformation($"StooqApi: getting stock {stockName} quote");

                var url = _configuration["StooqApiUrl"];

                using var message = new HttpRequestMessage(HttpMethod.Get, $"{url}q/l/?s={stockName}&f=sd2t2ohlcv&h&e=csv");
                message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));

                using var resp = await _httpClient.SendAsync(message);
                resp.EnsureSuccessStatusCode();

                using var s = await resp.Content.ReadAsStreamAsync();
                using var sr = new StreamReader(s);
                using var reader = new CsvReader(sr, CultureInfo.InvariantCulture);
                
                var records = reader.GetRecords<StockQuote>();
                var stockQuote = records.First();

                if (stockQuote.Close.Equals("N/D"))
                {
                    var msg = $"Stock {stockName} not found";
                    _logger.LogInformation($"StooqApi: {msg}");

                    return msg;
                }

                var response = $"{stockQuote.Symbol} quote is ${stockQuote.Close} per share";
                _logger.LogInformation($"StooqApi: {response}");

                return response;
            }
            catch (Exception e)
            {
                _logger.LogError($"StooqApi: {e.Message} | {e.StackTrace}");
                return "Oops, Something Went Wrong Please Try Again!";
            }
        }

        public class StockQuote
        {
            public string Symbol { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public string Open { get; set; }
            public string High { get; set; }
            public string Low { get; set; }
            public string Close { get; set; }
            public string Volume { get; set; }
        }

        
    }

   
}