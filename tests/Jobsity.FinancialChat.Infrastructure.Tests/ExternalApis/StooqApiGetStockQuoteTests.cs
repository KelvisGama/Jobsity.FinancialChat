using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Jobsity.FinancialChat.Infrastructure.ExternalApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Shouldly;
using Xunit;

namespace Jobsity.FinancialChat.Infrastructure.Tests.ExternalApis
{
    public class StooqApiGetStockQuoteTests
    {
        private readonly Mock<ILogger<StooqApi>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;

        private const string ValidContent =
            "Symbol,Date,Time,Open,High,Low,Close,Volume\n\r STOCK,2020-12-08,22:00:03,124.37,124.98,123.09,124.38,63103546";

        private const string InvalidContent =
            "Symbol,Date,Time,Open,High,Low,Close,Volume\n\r STOCK,N/D,N/D,N/D,N/D,N/D,N/D,N/D";

        public StooqApiGetStockQuoteTests()
        {
            _loggerMock = new Mock<ILogger<StooqApi>>();
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "StooqApiUrl")]).Returns("http://localhost");
        }

        [Fact]
        public async Task When_StooqApiReturns200_With_ValidContent_Should_ReturnTheQuote()
        {
            // arrange
            var handler = GetMockedHttpHandler(ValidContent);
            var httpClient = new HttpClient(handler.Object);
            var stooqApi = new StooqApi(_loggerMock.Object, httpClient, _configurationMock.Object);

            // act
            var result = await stooqApi.GetStockQuote("STOCK");

            // assert
            result.ShouldBe($"STOCK quote is $124.38 per share");

            handler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task When_StooqApiReturns200_With_InvalidContent_Should_ReturnNotFoundMessage()
        {
            // arrange
            var handler = GetMockedHttpHandler(InvalidContent);
            var httpClient = new HttpClient(handler.Object);
            var stooqApi = new StooqApi(_loggerMock.Object, httpClient, _configurationMock.Object);

            // act
            var result = await stooqApi.GetStockQuote("STOCK");

            // assert
            result.ShouldBe($"Stock STOCK not found");

            handler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task When_ThrowAnyException_Should_ReturnFriendlyErrorMessage()
        {
            // arrange
            var handler = GetMockedHttpHandler("exception content");
            var httpClient = new HttpClient(handler.Object);
            var stooqApi = new StooqApi(_loggerMock.Object, httpClient, _configurationMock.Object);

            // act
            var result = await stooqApi.GetStockQuote("STOCK");

            // assert
            result.ShouldBe($"Oops, Something Went Wrong Please Try Again!");

            handler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
        }

        private Mock<HttpMessageHandler> GetMockedHttpHandler(string content)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content),
            };

            handlerMock
                   .Protected()
                   .Setup<Task<HttpResponseMessage>>(
                       "SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(response);

            return handlerMock;
        }
    }
}