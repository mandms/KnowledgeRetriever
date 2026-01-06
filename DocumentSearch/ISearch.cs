namespace DocumentSearch
{
    public interface ISearch
    {
        public Task<string> Process(string search);
    }
}
