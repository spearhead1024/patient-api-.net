namespace PatientAdviceAPI.Interfaces
{
    public interface IAdviceService
    {
        Task<string> GenerateAdviceAsync(string patientId);
    }
}
