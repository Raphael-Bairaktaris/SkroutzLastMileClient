// Ignore Spelling: Skroutz

using Newtonsoft.Json;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// Returns tracking information on the parcel delivery and pickup.
    /// </summary>
    public class ShipmentTrackResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="CourierNotes"/>
        /// </summary>
        private string? mCourierNotes;

        /// <summary>
        /// The member of the <see cref="ShippingOrderId"/>
        /// </summary>
        private string? mShippingOrderId;

        #endregion

        #region Public Properties

        /// <summary>
        /// Indicates if the tracking information is successful
        /// </summary>
        [JsonProperty("success")]
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// The status description
        /// </summary>
        [JsonConverter(typeof(StatusDescriptionToStringJsonConverter))]
        [JsonProperty("status_description")]
        public StatusDescriptionType StatusDescription { get; set; }

        /// <summary>
        /// The courier notes
        /// </summary>
        [JsonProperty("courier_notes")]
        public string CourierNotes 
        {
            get => mCourierNotes ?? string.Empty;
            set => mCourierNotes = value;
        }

        /// <summary>
        /// The time of pickup
        /// </summary>
        [JsonProperty("picked_up_at")]
        public DateTimeOffset? PickedUpAt { get; set; }

        /// <summary>
        /// The time delivered at
        /// </summary>
        [JsonProperty("delivered_at")]
        public DateTimeOffset? DeliveredAt { get; set; }

        /// <summary>
        /// The time updated at
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The expected day of delivery
        /// </summary>
        [JsonProperty("expected_day_of_delivery")]
        public DateTimeOffset? ExpectedDayOfDelivery { get; set; }

        /// <summary>
        /// The shipping order id
        /// </summary>
        [JsonProperty("shipping_order_id")]
        public string ShippingOrderId 
        { 
            get => mShippingOrderId ?? string.Empty;
            set => mShippingOrderId = value;
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShipmentTrackResponseModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override string ToString() => $"Order Id: {ShippingOrderId}";

        #endregion
    }
}