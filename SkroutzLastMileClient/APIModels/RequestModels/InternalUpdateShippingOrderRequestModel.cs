// Ignore Spelling: Skroutz

using Newtonsoft.Json;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// Used to reschedule the pickup date and time window for an order. 
    /// Use the same instruction for these fields as in the order creation endpoint.
    /// </summary>
    internal class InternalUpdateShippingOrderRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The date of the order pickup
        /// </summary>
        [JsonConverter(typeof(DateOnlyToStringJsonConverter))]
        [JsonProperty("pickup_date")]
        public DateOnly? PickupDate { get; set; }

        /// <summary>
        /// The start time of the pickup window
        /// </summary>
        [JsonConverter(typeof(DateTimeToStringJsonConverter))]
        [JsonProperty("pickup_time_from")]
        public DateTime? PickupTimeFrom { get; set; }

        /// <summary>
        /// The end time of the pickup window
        /// </summary>
        [JsonConverter(typeof(DateTimeToStringJsonConverter))]
        [JsonProperty("pickup_time_to")]
        public DateTime? PickupTimeTo { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public InternalUpdateShippingOrderRequestModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="InternalUpdateShippingOrderRequestModel"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static InternalUpdateShippingOrderRequestModel FromUpdateShippingOrderRequestModel(UpdateShippingOrderRequestModel model)
        {
            return new InternalUpdateShippingOrderRequestModel()
            {
                PickupDate = model.PickupDate,
                PickupTimeFrom = model.PickupDate is null || model.PickupTimeFrom is null ? null : new DateTime(model.PickupDate.Value.Year, model.PickupDate.Value.Month, model.PickupDate.Value.Day, model.PickupTimeFrom.Value.Hour, model.PickupTimeFrom.Value.Minute, model.PickupTimeFrom.Value.Second),
                PickupTimeTo = model.PickupDate is null || model.PickupTimeTo is null ? null : new DateTime(model.PickupDate.Value.Year, model.PickupDate.Value.Month, model.PickupDate.Value.Day, model.PickupTimeTo.Value.Hour, model.PickupTimeTo.Value.Minute, model.PickupTimeTo.Value.Second),
            };
        }

        #endregion
    }
}