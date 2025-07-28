namespace PatientAdviceAPI.Models
{
    public class GlucoseReading
    {
        public DateTime Timestamp { get; set; }
        public int TimeIdx { get; set; }
        public decimal GlucoseValue { get; set; }
        public decimal GlucoseTrendNumeric { get; set; }
        public decimal CarbsGrams { get; set; }

        public GlucoseReading(DateTime timestamp, int timeIdx, decimal glucoseValue, decimal glucoseTrendNumeric, decimal carbsGrams)
        {
            Timestamp = timestamp;
            TimeIdx = timeIdx;
            GlucoseValue = glucoseValue;
            GlucoseTrendNumeric = glucoseTrendNumeric;
            CarbsGrams = carbsGrams;
        }
    }
}
