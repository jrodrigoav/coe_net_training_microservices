namespace InventoryAPI.Contracts
{
    public interface IHttpRespondeReader
    {
        Task<string> ReadResponseAsync(string url);
    }
}
