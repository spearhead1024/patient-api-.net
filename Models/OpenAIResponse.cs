namespace PatientAdviceAPI.Models
{
    public class OpenAIResponse
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public long Created { get; set; }
        public string Model { get; set; }

        public UsageInfo Usage { get; set; }
        public List<Choice> Choices { get; set; }

        public class UsageInfo
        {
            public int PromptTokens { get; set; }
            public int CompletionTokens { get; set; }
            public int TotalTokens { get; set; }
        }

        public class Choice
        {
            public Message Message { get; set; }
            public string FinishReason { get; set; }
            public int Index { get; set; }
        }

        public class Message
        {
            public string Role { get; set; }
            public string Content { get; set; }
        }
    }
}
