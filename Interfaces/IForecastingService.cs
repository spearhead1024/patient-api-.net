using PatientAdviceAPI.Models;

namespace PatientAdviceAPI.Interfaces
{
    public interface IForecastingService
    {
        Task<ForecastResult> GetForecastAsync(IReadOnlyList<GlucoseReading> readings);
    }
}
