using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
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
        internal static string ToSHA1(this string s)
        {
            using (var provider = new SHA1Managed())
            {
                var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(s));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (var @byte in hash)
                {
                    sb.Append(@byte.ToString("X2"));
                }

                return sb.ToString();
            }
           
        }
        public static string First5(this string s)
        {
            return s.Substring(0, 5);
        }

        public static bool IsNullEmptyOrWhitespace(this string s)
        {
            return string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);
        }

        public static string ToBooleanString(this bool b)
        {
            return b == true ? "true" : "false";
        }
    }
}
