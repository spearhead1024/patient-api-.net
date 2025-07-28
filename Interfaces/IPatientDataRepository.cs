using System.Collections.Generic;
using System.Threading.Tasks;
using PatientAdviceAPI.Models;

namespace PatientAdviceAPI.Interfaces
{
    public interface IPatientDataRepository
    {
        Task<IReadOnlyList<GlucoseReading>> GetRecentReadingsAsync(string patientId, int limit = 72);
    }
}
