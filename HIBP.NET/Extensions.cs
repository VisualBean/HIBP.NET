using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HIBP.Extensions
{
    internal static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }
    }
    internal static class StringExtensions
    {
        public static string ToBooleanString(this bool b)
        {
            return b == true ? "true" : "false";
        }
    }
}
