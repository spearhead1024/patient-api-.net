using PatientAdviceAPI.Interfaces;
using PatientAdviceAPI.Models;

namespace PatientAdviceAPI.Services
{
    public class MockForecastingService : IForecastingService
    {
        public Task<ForecastResult> GetForecastAsync(IReadOnlyList<GlucoseReading> readings)
        {
            var forecastedValues = readings.Select(r => r.GlucoseValue + 0.5m).ToList();
            return Task.FromResult(new ForecastResult(forecastedValues));
        }
    }
}
