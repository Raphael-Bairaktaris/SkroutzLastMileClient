using Microsoft.AspNetCore.WebUtilities;
using SkroutzLastMileClient.ResponseModels;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// An agent that is capable of making requests to the Skroutz Last Mile API
    /// </summary>
    public class SkroutzLastMileClient
    {
        #region Public Properties

        /// <summary>
        /// The client
        /// </summary>
        public WebRequestsClient Client { get; }

        /// <summary>
        /// The credentials
        /// </summary>
        public SkroutzLastMileCredentials Credentials { get; }

        /// <summary>
        /// The base route
        /// </summary>
        public string BaseRoute { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SkroutzLastMileClient(SkroutzLastMileCredentials credentials, bool shouldUseTestAPI = false) : base()
        {
            Client = WebRequestsClient.Instance;

            Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            BaseRoute = shouldUseTestAPI ? SkroutzLastMileRoutes.StageRoute : Credentials.APIURL;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a shipping order
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public async Task<WebRequestResult<ShippingOrderResponseModel>> CreateShippingOrderAsync(CreateShippingOrderRequestModel model)
        {
            var result = await Client.PostAsync<InternalShippingOrderResponseModel>(SkroutzLastMileRoutes.GetNewShippingOrderRoute(BaseRoute), InternalCreateShippingOrderRequestModel.FromCreateShippingOrderRequestModel(model), Credentials.APIKey);

            if (!result.IsSuccessful)
                return result.ToUnsuccessfulWebRequestResult<ShippingOrderResponseModel>();

            return result.ToSuccessfulWebRequestResult(x => x.ToShippingOrderResponseModel());
        }

        /// <summary>
        /// Tracks a shipment
        /// </summary>
        /// <param name="trackingId">The tracking id</param>
        /// <returns></returns>
        public Task<WebRequestResult<ShipmentTrackResponseModel>> TrackShipmentAsync(string trackingId)
            => Client.GetAsync<ShipmentTrackResponseModel>(SkroutzLastMileRoutes.GetShipmentTrackRoute(BaseRoute, trackingId), Credentials.APIKey);

        /// <summary>
        /// Deletes an order
        /// </summary>
        /// <param name="orderId">The order id</param>
        /// <returns></returns>
        public Task<WebRequestResult<DeleteOrderResponseModel>> DeleteOrderAsync(string orderId)
            => Client.DeleteAsync<DeleteOrderResponseModel>(SkroutzLastMileRoutes.GetShippingOrderRoute(BaseRoute, orderId), Credentials.APIKey);

        /// <summary>
        /// Updates pickup date and time window
        /// </summary>
        /// <param name="orderId">The order id</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public Task<WebRequestResult<ShippingOrderResponseModel>> UpdateShippingOrderAsync(string orderId, UpdateShippingOrderRequestModel model)
            => Client.PutAsync<ShippingOrderResponseModel>(SkroutzLastMileRoutes.GetShippingOrderRoute(BaseRoute, orderId), InternalUpdateShippingOrderRequestModel.FromUpdateShippingOrderRequestModel(model), Credentials.APIKey);

        /// <summary>
        /// Gets the voucher related to the specified <paramref name="trackingId"/> as a byte array
        /// that represents a PDF file
        /// </summary>
        /// <param name="trackingId">The tracking id</param>
        /// <param name="paperSize">The paper size</param>
        /// <returns></returns>
        public Task<IFailable<byte[]>> GetShippingOrderVoucherAsPDFAsync(string trackingId, PaperSize paperSize)
        {
            var url = SkroutzLastMileRoutes.GetShippingOrderVoucherRoute(BaseRoute, trackingId);

            url = QueryHelpers.AddQueryString(url, SkroutzLastMileClientConstants.DirectDownloadQueryArgumentName, "true");
            url = QueryHelpers.AddQueryString(url, SkroutzLastMileClientConstants.PaperSizeQueryArgumentName, SkroutzLastMileClientConstants.PaperSizeToStringMapper[paperSize]);

            return Client.GetBytesAsync(url, Credentials.APIKey);
        }

        /// <summary>
        /// Gets the voucher related to the specified <paramref name="trackingId"/> as a Base64 string
        /// that represents a PDF file
        /// </summary>
        /// <param name="trackingId">The tracking id</param>
        /// <param name="paperSize">The paper size</param>
        /// <returns></returns>
        public async Task<IFailable<string>> GetShippingOrderVoucherAsBase64StringAsync(string trackingId, PaperSize paperSize)
        {
            var url = SkroutzLastMileRoutes.GetShippingOrderVoucherRoute(BaseRoute, trackingId);

            url = QueryHelpers.AddQueryString(url, SkroutzLastMileClientConstants.DirectDownloadQueryArgumentName, "false");
            url = QueryHelpers.AddQueryString(url, SkroutzLastMileClientConstants.PaperSizeQueryArgumentName, SkroutzLastMileClientConstants.PaperSizeToStringMapper[paperSize]);

            var result = await Client.GetBytesAsync(url, Credentials.APIKey);

            if (!result.IsSuccessful)
                return new Failable<string>() { ErrorMessage = result.ErrorMessage };

            return new Failable<string>() { Result = Convert.ToBase64String(result.Result) };
        }

        /// <summary>
        /// Gets all the Skroutz points
        /// </summary>
        /// <returns></returns>
        public async Task<WebRequestResult<IEnumerable<SkroutzPointResponseModel>>> GetAllSkroutzPointsAsync()
        {
            var result = await Client.GetAsync<InternalSkroutzPointResponseModel>(SkroutzLastMileRoutes.GetAllSkroutzPointsRoute(BaseRoute), Credentials.APIKey);

            if (!result.IsSuccessful)
                return result.ToUnsuccessfulWebRequestResult<IEnumerable<SkroutzPointResponseModel>>();

            return result.ToSuccessfulWebRequestResult(x => x.SkroutzPoints);
        }
        #endregion
    }
}
