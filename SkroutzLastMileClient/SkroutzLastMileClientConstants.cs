using System.Collections.Immutable;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// Constants related to Skroutz last mile client
    /// </summary>
    public static class SkroutzLastMileClientConstants
    {
        /// <summary>
        /// The query argument that is used when retrieving a voucher PDF and is used for determining the encoding
        /// of the returned data.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If set <see cref="true"/> then the voucher is returned as a stream representing the PDF file.
        /// </para>
        /// <para>
        /// If set <see cref="false"/> then the voucher PDF is returned as a Base64 encoded string.
        /// </para>
        /// </remarks>
        public const string DirectDownloadQueryArgumentName = "direct_download";

        /// <summary>
        /// The query argument that is used for determining the size of the voucher that is generated
        /// </summary>
        public const string PaperSizeQueryArgumentName = "paper_size";

        /// <summary>
        /// Maps the <see cref="DayOfWeek"/> to their related <see cref="string"/>s
        /// </summary>
        public static IReadOnlyDictionary<DayOfWeek, string> DayOfWeekToStringMapper { get; } = Enum.GetValues<DayOfWeek>().ToDictionary(x => x, x => x.ToString()).ToImmutableDictionary();

        /// <summary>
        /// Maps the <see cref="StatusDescriptionType"/>s to their related <see cref="string"/>s
        /// </summary>
        public static IReadOnlyDictionary<StatusDescriptionType, string> StatusDescriptionTypeToStringMapper { get; } = new Dictionary<StatusDescriptionType, string>()
        {
            { StatusDescriptionType.AwaitingPickup, "awaiting_pickup" },
            { StatusDescriptionType.PickedUp, "picked_up" },
            { StatusDescriptionType.AssignedToCourierForDelivery, "assigned_to_courier_fro_delivery" },
            { StatusDescriptionType.Delivered, "delivered" },
            { StatusDescriptionType.RecipientRejected, "recipient_rejected" },
            { StatusDescriptionType.ReturnedToSender, "returned_to_sender" },
            { StatusDescriptionType.ReturningToSender, "returning_to_sender" },
            { StatusDescriptionType.AttemptedDelivery, "attempted_delivery" },
            { StatusDescriptionType.AtLocker, "at_locker" }
        }.ToImmutableDictionary();

        /// <summary>
        /// Maps the <see cref="PaperSize"/>s to their related <see cref="string"/>s
        /// </summary>
        public static IReadOnlyDictionary<PaperSize, string> PaperSizeToStringMapper { get; } = new Dictionary<PaperSize, string>()
        {
            { PaperSize.A4, "A4" },
            { PaperSize.Thermal, "thermal" },
        }.ToImmutableDictionary();
    }
}