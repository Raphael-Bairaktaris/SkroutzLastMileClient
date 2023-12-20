using System.Collections.Immutable;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// Constants related to Skroutz last mile client
    /// </summary>
    public static class SkroutzLastMileClientConstants
    {
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
    }
}