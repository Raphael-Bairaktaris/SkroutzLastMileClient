namespace SkroutzLastMileClient
{
    /// <summary>
    /// The API routes of the Skroutz Last Mile API
    /// </summary>
    public static class SkroutzLastMileRoutes
    {
        /// <summary>
        /// The stage (testing) route for the API
        /// </summary>
        public const string StageRoute = "https://staging-api.skroutzdelivery.gr";

        /// <summary>
        /// The API route
        /// </summary>
        /// <param name="baseRoute">The base route</param>
        /// <returns></returns>
        public static string GetAPIRoute(string baseRoute) => baseRoute + "/api/v1";

        /// <summary>
        /// The new shipping order route
        /// </summary>
        /// <param name="baseRoute">The base route</param>
        /// <returns></returns>
        public static string GetNewShippingOrderRoute(string baseRoute) => GetAPIRoute(baseRoute) + "/shipping_order";

        /// <summary>
        /// The shipping track route
        /// </summary>
        /// <param name="baseRoute">The base route</param>
        /// <param name="trackingId">The tracking id</param>
        /// <returns></returns>
        public static string GetShipmentTrackRoute(string baseRoute, string trackingId) => GetAPIRoute(baseRoute) + $"/shipment/track/{trackingId}";

        /// <summary>
        /// The shipping order route
        /// </summary>
        /// <param name="baseRoute">The base route</param>
        /// <param name="orderId">The order id</param>
        /// <returns></returns>
        public static string GetShippingOrderRoute(string baseRoute, string orderId) => GetAPIRoute(baseRoute) + $"/shipping_order/{orderId}";

        /// <summary>
        /// The shipping order voucher route
        /// </summary>
        /// <param name="baseRoute">The base route</param>
        /// <param name="trackingId">The tracking id</param>
        /// <returns></returns>
        public static string GetShippingOrderVoucherRoute(string baseRoute, string trackingId) => GetAPIRoute(baseRoute) + $"/shipping_order/voucher/{trackingId}";

        /// <summary>
        /// The all Skroutz points route
        /// </summary>
        /// <param name="baseRoute">The base route</param>
        /// <returns></returns>
        public static string GetAllSkroutzPointsRoute(string baseRoute) => baseRoute + "/mapwidget/list_all";
    }
}