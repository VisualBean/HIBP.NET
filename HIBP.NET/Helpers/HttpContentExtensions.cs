namespace HIBP.Helpers
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    internal static class HttpContentExtensions
    {
        /// <summary>
        /// Reads as json asynchronous.
        /// </summary>
        /// <typeparam name="T">The type to deserialize into.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns>A deserialized result of type <see cref="Task{TResult}".></returns>
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }
    }
}