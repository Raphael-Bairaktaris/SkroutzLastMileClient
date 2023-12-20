namespace SkroutzLastMileClient
{
    /// <summary>
    /// Provides enumeration over the error types
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// A standard error
        /// </summary>
        Error = 0,

        /// <summary>
        /// A warning
        /// </summary>
        Warning = 1,

        /// <summary>
        /// An informative message
        /// </summary>
        Information = 2,

        /// <summary>
        /// An error that should get hidden
        /// </summary>
        Hidden = 3
    }
}
