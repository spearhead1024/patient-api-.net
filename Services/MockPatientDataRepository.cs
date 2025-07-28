using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientAdviceAPI.Interfaces;
using PatientAdviceAPI.Models;

namespace PatientAdviceAPI.Services
{
    public class MockPatientDataRepository : IPatientDataRepository
    {
        public Task<IReadOnlyList<GlucoseReading>>
            GetRecentReadingsAsync(string patientId, int limit = 72)
        {
            var readings = new List<GlucoseReading>();
            var now = DateTime.UtcNow;
            var random = new Random();

            for (int i = 0; i < limit; i++)
            {
                var timestamp = now.AddMinutes(-i * 5);
                readings.Add(new GlucoseReading(
                    timestamp,
                    timeIdx: 1000 - i,
                    glucoseValue: 6.5m + (decimal)(random.NextDouble() - 0.5),
                    glucoseTrendNumeric: 0.0m,
                    carbsGrams: 0.0m
                ));
            }

            readings.Reverse();
            return Task.FromResult<IReadOnlyList<GlucoseReading>>(readings);
        }
    }
}
