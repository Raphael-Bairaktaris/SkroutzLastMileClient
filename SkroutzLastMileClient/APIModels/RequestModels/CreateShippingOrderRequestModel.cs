namespace SkroutzLastMileClient
{
    /// <summary>
    /// Request model used for creating a shipping order
    /// </summary>
    public class CreateShippingOrderRequestModel : UpdateShippingOrderRequestModel
    {
        #region Public Properties

        /// <summary>
        /// The customer reference (Your internal unique identifier of the order)
        /// </summary>
        public string? CustomerReference { get; set; }

        /// <summary>
        /// Indicates if the shipment is a return (Always use false)
        /// </summary>
        public bool? IsReturn { get; set; }

        /// <summary>
        /// The number of parcels (Always use 1. Multiple-package orders are not supported for locker deliveries.)
        /// </summary>
        public int? NumberOfParcels { get; set; }

        /// <summary>
        /// The unique code for the pickup location. It corresponds to a pre-agreed location for picking up parcels. 
        /// A merchant can have multiple pickup location codes where each one corresponds to a different pickup location.
        /// </summary>
        public string? PickupLocationCode { get; set; }

        /// <summary>
        /// Any additional notes for the pickup.
        /// </summary>
        public string? PickupNotes { get; set; }

        /// <summary>
        /// The full name of the recipient
        /// </summary>
        public string? RecipientName { get; set; }

        /// <summary>
        /// The mobile phone number of the recipient
        /// </summary>
        public string? RecipientPhone { get; set; }

        /// <summary>
        /// The phone number of the merchant
        /// </summary>
        public string? SenderPhone { get; set; }

        /// <summary>
        /// The Id of the chosen Skroutz Point locker from the map
        /// </summary>
        public string? SkroutzPointId { get; set; }

        /// <summary>
        /// The weight of the parcel
        /// </summary>
        public double? Weight { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public CreateShippingOrderRequestModel() : base()
        {

        }

        #endregion
    }
}