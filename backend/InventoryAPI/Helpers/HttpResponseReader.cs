using InventoryAPI.Contracts;

namespace InventoryAPI.Helpers
{
    public class HttpResponseReader : IHttpRespondeReader
    {
        public async Task<string> ReadResponseAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();

                    using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string data = "";
                        while (!streamReader.EndOfStream)
                        {
                            var chunk = await streamReader.ReadLineAsync();
                            data += chunk;
                        }
                        return data;
                    }
                }
            }
        }
    }
}
