namespace HIBP
{
    using System;

    /// <summary>
    /// An Invalid Api Key Exception.
    /// </summary>
    /// <seealso cref="System.ArgumentException" />
    public class InvalidApiKeyException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidApiKeyException"/> class.
        /// </summary>
        public InvalidApiKeyException()
            : base("To interact with the HIBP API a valid API-Key must be provided.")
        {
        }
    }

    /// <summary>
    /// An Invalid Service Name Exception.
    /// </summary>
    /// <seealso cref="System.ArgumentException" />
    public class InvalidServiceNameException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidServiceNameException"/> class.
        /// </summary>
        public InvalidServiceNameException()
            : base("To interact with the HIBP API a name must be provided for the useragent string. This name is ment to be to distinguish your service from others.")
        {
        }
    }

    /// <summary>
    /// A Too Many Requests Exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class TooManyRequestsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class.
        /// </summary>
        public TooManyRequestsException()
            : base("Your request has been throttled, please try again later")
        {
        }
    }
}
