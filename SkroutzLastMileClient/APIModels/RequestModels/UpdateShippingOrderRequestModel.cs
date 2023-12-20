// Ignore Spelling: Skroutz

using Newtonsoft.Json;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// Used to reschedule the pickup date and time window for an order. 
    /// Use the same instruction for these fields as in the order creation endpoint.
    /// </summary>
    public class UpdateShippingOrderRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The date of the order pickup
        /// </summary>
        [JsonConverter(typeof(DateOnlyToStringJsonConverter))]
        public DateOnly? PickupDate { get; set; }

        /// <summary>
        /// The start time of the pickup window
        /// </summary>
        public DateTimeOffset? PickupTimeFrom { get; set; }

        /// <summary>
        /// The end time of the pickup window
        /// </summary>
        public DateTimeOffset? PickupTimeTo { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateShippingOrderRequestModel() : base()
        {

        }

        #endregion
    }
}