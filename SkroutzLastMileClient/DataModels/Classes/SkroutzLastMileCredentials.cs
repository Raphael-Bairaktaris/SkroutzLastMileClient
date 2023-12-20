namespace SkroutzLastMileClient
{
    /// <summary>
    /// The Skroutz Last Mile credentials
    /// </summary>
    public record SkroutzLastMileCredentials
    {
        #region Public Properties

        /// <summary>
        /// The API key
        /// </summary>
        public string APIKey { get; }

        /// <summary>
        /// The API url
        /// </summary>
        public string APIURL { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="apiURL">The API url</param>
        public SkroutzLastMileCredentials(string apiKey, string apiURL)
        {
            APIKey = apiKey ?? string.Empty;
            APIURL = apiURL ?? string.Empty;
            if (!APIURL.StartsWith("https://"))
                APIURL = "https://" + APIURL;
        }

        #endregion
    }
}
