namespace SkroutzLastMileClient
{
    /// <summary>
    /// Provides enumeration for status description type
    /// </summary>
    public enum StatusDescriptionType
    {
        /// <summary>
        /// Parcel Awaiting pickup
        /// </summary>
        AwaitingPickup = 0,

        /// <summary>
        /// Parcel picked up
        /// </summary>
        PickedUp = 1,

        /// <summary>
        /// Parcel assigned to courier for delivery
        /// </summary>
        AssignedToCourierForDelivery = 2,

        /// <summary>
        /// Parcel delivered
        /// </summary>
        Delivered = 3,

        /// <summary>
        /// Parcel recipient rejected
        /// </summary>
        RecipientRejected = 4,

        /// <summary>
        /// Parcel returned to sender
        /// </summary>
        ReturnedToSender = 5,

        /// <summary>
        /// Parcel returning to sender
        /// </summary>
        ReturningToSender = 6,

        /// <summary>
        /// Parcel attempted delivery
        /// </summary>
        AttemptedDelivery = 7,

        /// <summary>
        /// Parcel lost or damaged
        /// </summary>
        LostOrDamaged = 8,

        /// <summary>
        /// Parcel at locker
        /// </summary>
        AtLocker = 9
    }
}
