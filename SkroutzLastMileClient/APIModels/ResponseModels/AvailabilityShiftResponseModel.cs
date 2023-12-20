// Ignore Spelling: Skroutz

using Newtonsoft.Json;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// Provides information about the working hours
    /// </summary>
    public class AvailabilityShiftResponseModel
    {
        #region Public Properties

        /// <summary>
        /// The time when the locker is available from
        /// </summary>
        [JsonProperty("from")]
        [JsonConverter(typeof(TimeOnlyToStringJsonConverter))]
        public TimeOnly From { get; set; }

        /// <summary>
        /// The time when the locker is available to
        /// </summary>
        [JsonProperty("to")]
        [JsonConverter(typeof(TimeOnlyToStringJsonConverter))]
        public TimeOnly To { get; set; }

        /// <summary>
        /// The day the locker is available
        /// </summary>
        [JsonProperty("day")]
        [JsonConverter(typeof(DayfOfWeekToStringJsonConverter))]
        public DayOfWeek DayOfWeek { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AvailabilityShiftResponseModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override string ToString() => $"{From} to {To} each {DayOfWeek}";

        #endregion
    }
}
