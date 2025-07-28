namespace PatientAdviceAPI.Interfaces
{
    public interface ILLMService
    {
        Task<string> GetAdviceAsync(string prompt);
    }
}
