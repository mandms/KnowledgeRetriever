namespace DocumentSearch
{
    public interface ILLMService
    {
        public Task<string> GetCompletionAsync(string prompt);
    }
}
