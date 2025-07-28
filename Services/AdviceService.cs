using PatientAdviceAPI.Interfaces;

namespace PatientAdviceAPI.Services
{
    public class AdviceService : IAdviceService
    {
        private readonly ILLMService _llmService;
        private readonly IPatientDataRepository _patientDataRepository;
        private readonly IForecastingService _forecastingService;

        public AdviceService(
            ILLMService llmService,
            IPatientDataRepository patientDataRepository,
            IForecastingService forecastingService)
        {
            _llmService = llmService;
            _patientDataRepository = patientDataRepository;
            _forecastingService = forecastingService;
        }

        public async Task<string> GenerateAdviceAsync(string patientId)
        {
            var readings = await _patientDataRepository.GetRecentReadingsAsync(patientId);
            var forecast = await _forecastingService.GetForecastAsync(readings);

            var prompt = $"Patient {patientId} glucose forecast: {string.Join(", ", forecast.ForecastedValues)}. Provide health advice.";

            var advice = await _llmService.GetAdviceAsync(prompt);

            return advice;
        }
    }
}
