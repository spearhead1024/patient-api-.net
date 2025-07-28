namespace PatientAdviceAPI.Models
{
    public class ForecastResult
    {
        public List<decimal> ForecastedValues { get; set; }

        public ForecastResult(List<decimal> forecastedValues)
        {
            ForecastedValues = forecastedValues;
        }
    }
}
