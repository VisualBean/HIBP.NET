namespace HIBP.Helpers
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// String extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Gets first 5 characters of a string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        /// The first 5 characters of a string.
        /// </returns>
        internal static string First5(this string s)
        {
            return s.Substring(0, 5);
        }

        /// <summary>
        /// Converts to a boolean string.
        /// </summary>
        /// <param name="b">if set to <c>true</c> [b].</param>
        /// <returns>A string representing the boolean value.</returns>
        internal static string ToBooleanString(this bool b)
        {
            return b == true ? "true" : "false";
        }

        /// <summary>
        /// Converts to sha1.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A sha1 hashed string.</returns>
        public static string ToSHA1(this string s)
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
    }
}
