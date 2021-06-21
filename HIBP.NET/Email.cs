namespace HIBP
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A struct representing an Email address.
    /// </summary>
    public struct Email
    {
        private string email;

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> struct.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <exception cref="ArgumentException">Email is not valid.</exception>
        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email must be supplied.");
            }

            if (!ValidateEmail(email))
            {
                throw new ArgumentException("Email is not valid.", nameof(email));
            }

            this.email = email;
        }

        public static implicit operator string(Email email) => email.ToString();

        public static bool operator !=(Email left, Email right)
        {
            return !(left == right);
        }

        public static bool operator ==(Email left, Email right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Validates the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>A bool indicating validation success.</returns>
        public static bool ValidateEmail(string email)
        {
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"

               + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"

               + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"

               + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"

               + @"[a-zA-Z]{2,}))$";

            Regex regexStrict = new Regex(patternStrict, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(1));

            return regexStrict.IsMatch(email);
        }

        /// <summary>
        /// Equals the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>A bool representing equality.</returns>
        public override bool Equals(object obj)
        {
            return obj as string == this.email;
        }

        /// <summary>
        /// Gets the hash code.
        /// </summary>
        /// <returns>The hashcode.</returns>
        public override int GetHashCode()
        {
            return this.email.GetHashCode();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref=string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.email;
        }
    }
}
