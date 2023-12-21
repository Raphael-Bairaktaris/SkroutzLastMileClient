using Newtonsoft.Json;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// Cancel an order any time before it is received by the customer.
    /// </summary>
    public class DeleteOrderResponseModel
    {
        #region Public Properties

        /// <summary>
        /// Indicates if the order is canceled before reaching to customer
        /// </summary>
        [JsonProperty("success")]
        public bool IsSuccessful { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DeleteOrderResponseModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override string ToString() => $"Successful: {IsSuccessful}";

        #endregion
    }
}