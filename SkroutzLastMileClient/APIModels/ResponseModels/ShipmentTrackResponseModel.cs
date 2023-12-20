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
        /// The member of the <see cref="TrackingId"/>
        /// </summary>
        private string? mTrackingId;

        #endregion

        #region Public Properties

        /// <summary>
        /// The tracking ID of the shipment
        /// </summary>
        [JsonProperty("tracking_id")]
        public string TrackingId
        {
            get => mTrackingId ?? string.Empty;
            set => mTrackingId = value;
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
        public override string ToString() => $"Tracking id: {mTrackingId}";

        #endregion
    }
}