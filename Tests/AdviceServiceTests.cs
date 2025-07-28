using Moq;
using PatientAdviceAPI.Interfaces;
using PatientAdviceAPI.Models;
using PatientAdviceAPI.Services;
using Xunit;

namespace PatientAdviceAPI.Tests
{
    public class AdviceServiceTests
    {
        [Fact]
        public async Task GenerateAdviceAsync_ShouldReturnAdvice()
        {
            var mockLLMService = new Mock<ILLMService>();
            var mockPatientDataRepository = new Mock<IPatientDataRepository>();
            var mockForecastingService = new Mock<IForecastingService>();

            mockPatientDataRepository
                .Setup(repo => repo.GetRecentReadingsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<GlucoseReading> { new GlucoseReading(DateTime.UtcNow, 1, 7.5m, 0.5m, 20) });

            mockForecastingService
                .Setup(service => service.GetForecastAsync(It.IsAny<IReadOnlyList<GlucoseReading>>()))
                .ReturnsAsync(new ForecastResult(new List<decimal> { 7.8m }));

            mockLLMService
                .Setup(service => service.GetAdviceAsync(It.IsAny<string>()))
                .ReturnsAsync("Advice: Stay healthy!");

            var adviceService = new AdviceService(mockLLMService.Object, mockPatientDataRepository.Object, mockForecastingService.Object);

            var result = await adviceService.GenerateAdviceAsync("patientId");

            Assert.Equal("Advice: Stay healthy!", result);
        }
    }
}
