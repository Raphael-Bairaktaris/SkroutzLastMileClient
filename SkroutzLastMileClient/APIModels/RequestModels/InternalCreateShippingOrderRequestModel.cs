// Ignore Spelling: Skroutz

using Newtonsoft.Json;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// Request model used for creating a shipping order
    /// </summary>
    internal class InternalCreateShippingOrderRequestModel : InternalUpdateShippingOrderRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The customer reference (Your internal unique identifier of the order)
        /// </summary>
        [JsonProperty("customer_reference")]
        public string? CustomerReference { get; set; }

        /// <summary>
        /// Indicates if the shipment is a return (Always use false)
        /// </summary>
        [JsonProperty("is_return")]
        public bool? IsReturn { get; set; }

        /// <summary>
        /// The number of parcels (Always use 1. Multiple-package orders are not supported for locker deliveries.)
        /// </summary>
        [JsonProperty("number_of_parcels")]
        public int? NumberOfParcels { get; set; }

        /// <summary>
        /// The unique code for the pickup location. It corresponds to a pre-agreed location for picking up parcels. 
        /// A merchant can have multiple pickup location codes where each one corresponds to a different pickup location.
        /// </summary>
        [JsonProperty("pickup_location_code")]
        public string? PickupLocationCode { get; set; }

        /// <summary>
        /// Any additional notes for the pickup.
        /// </summary>
        [JsonProperty("pickup_notes")]
        public string? PickupNotes { get; set; }

        /// <summary>
        /// The full name of the recipient
        /// </summary>
        [JsonProperty("recipient_name")]
        public string? RecipientName { get; set; }

        /// <summary>
        /// The mobile phone number of the recipient
        /// </summary>
        [JsonProperty("recipient_phone")]
        public string? RecipientPhone { get; set; }

        /// <summary>
        /// The phone number of the merchant
        /// </summary>
        [JsonProperty("sender_phone")]
        public string? SenderPhone { get; set; }

        /// <summary>
        /// The Id of the chosen Skroutz Point locker from the map
        /// </summary>
        [JsonProperty("skroutz_point_id")]
        public string? SkroutzPointId { get; set; }

        /// <summary>
        /// The weight of the parcel
        /// </summary>
        [JsonProperty("weight")]
        public int? Weight { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public InternalCreateShippingOrderRequestModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="InternalCreateShippingOrderRequestModel"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static InternalCreateShippingOrderRequestModel FromCreateShippingOrderRequestModel(CreateShippingOrderRequestModel model)
        {
            return new InternalCreateShippingOrderRequestModel()
            {
                PickupDate = model.PickupDate,
                PickupTimeFrom = model.PickupDate is null || model.PickupTimeFrom is null ? null : new DateTime(model.PickupDate.Value.Year, model.PickupDate.Value.Month, model.PickupDate.Value.Day, model.PickupTimeFrom.Value.Hour, model.PickupTimeFrom.Value.Minute, model.PickupTimeFrom.Value.Second),
                PickupTimeTo = model.PickupDate is null || model.PickupTimeTo is null ? null : new DateTime(model.PickupDate.Value.Year, model.PickupDate.Value.Month, model.PickupDate.Value.Day, model.PickupTimeTo.Value.Hour, model.PickupTimeTo.Value.Minute, model.PickupTimeTo.Value.Second),
                CustomerReference = model.CustomerReference,
                IsReturn = model.IsReturn,
                NumberOfParcels = model.NumberOfParcels,
                PickupLocationCode = model.PickupLocationCode,
                PickupNotes = model.PickupNotes,
                RecipientName = model.RecipientName,
                RecipientPhone = model.RecipientPhone,
                SenderPhone = model.SenderPhone,
                SkroutzPointId = model.SkroutzPointId,
                Weight = model.Weight
            };
        }

        #endregion
    }
}