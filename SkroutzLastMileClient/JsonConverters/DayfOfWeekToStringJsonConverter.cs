using Newtonsoft.Json;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// The <see cref="JsonConverter{T}"/> that converts a <see cref="DayOfWeek"/> to <see cref="string"/>s
    /// </summary>
    public class DayfOfWeekToStringJsonConverter : BaseEnumJsonConverter<DayOfWeek>
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DayfOfWeekToStringJsonConverter()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override IReadOnlyDictionary<DayOfWeek, string> GetMapper() => SkroutzLastMileClientConstants.DayOfWeekToStringMapper;

        #endregion
    }
}