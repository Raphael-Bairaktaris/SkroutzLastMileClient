using Newtonsoft.Json;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// The shipping order response
    /// </summary>
    internal class InternalShippingOrderResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="OrderId"/> property
        /// </summary>
        private string? mOrderId;

        /// <summary>
        /// The member of the <see cref="TrackingIds"/> property
        /// </summary>
        private IEnumerable<string>? mTrackingIds;

        #endregion

        #region Public Properties

        /// <summary>
        /// Indicates if the creation of the shipping order is successful
        /// </summary>
        [JsonProperty("success")]
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// The order id
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId
        {
            get => mOrderId ?? string.Empty;
            set => mOrderId = value;
        }

        /// <summary>
        /// The tracking id
        /// </summary>
        [JsonProperty("tracking_ids")]
        public IEnumerable<string> TrackingIds
        {
            get => mTrackingIds ?? Enumerable.Empty<string>();
            set => mTrackingIds = value;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public InternalShippingOrderResponseModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="ShippingOrderResponseModel"/> from the current <see cref="InternalShippingOrderResponseModel"/>
        /// </summary>
        /// <returns></returns>
        public ShippingOrderResponseModel ToShippingOrderResponseModel()
        {
            return new ShippingOrderResponseModel()
            {
                IsSuccessful = IsSuccessful,
                OrderId = OrderId,
                TrackingId = TrackingIds.FirstOrDefault()
            };
        }

        #endregion
    }
}
