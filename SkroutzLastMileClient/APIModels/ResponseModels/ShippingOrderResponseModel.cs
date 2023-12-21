using System.Diagnostics.CodeAnalysis;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// The shipping order response
    /// </summary>
    public class ShippingOrderResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="OrderId"/> property
        /// </summary>
        private string? mOrderId;

        /// <summary>
        /// The member of the <see cref="TrackingId"/> property
        /// </summary>
        private string? mTrackingId;

        #endregion

        #region Public Properties

        /// <summary>
        /// Indicates if the creation of the shipping order is successful
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// The order id
        /// </summary>
        [AllowNull]
        public string OrderId
        {
            get => mOrderId ?? string.Empty;
            set => mOrderId = value;
        }

        /// <summary>
        /// The tracking id
        /// </summary>
        [AllowNull]
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
        public ShippingOrderResponseModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override string ToString() => $"TrackingId: {TrackingId}, OrderId: {OrderId} ";

        #endregion
    }
}
