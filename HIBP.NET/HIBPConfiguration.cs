namespace HIBP
{
    /// <summary>
    /// HIBP Configuration, to hold API key and Service name.
    /// </summary>
    public class HIBPConfiguration
    {
        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public ApiKey ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        public string ServiceName { get; set; }
    }
}
