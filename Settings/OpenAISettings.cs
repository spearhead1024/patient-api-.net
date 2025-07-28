namespace PatientAdviceAPI.Settings
{
    public class OpenAISettings
    {
        public const string SectionName = "OpenAI";

        public string ApiKey { get; set; } = string.Empty;
        public string ModelName { get; set; } = "gpt-3.5-turbo";
        public string BaseUrl { get; set; } = "https://api.openai.com/v1";
        public int MaxTokens { get; set; } = 150;
        public double Temperature { get; set; } = 0.7;
    }
}
