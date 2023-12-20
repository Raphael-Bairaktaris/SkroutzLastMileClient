using Newtonsoft.Json;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// The <see cref="JsonConverter{T}"/> that converts a <see cref="StatusDescriptionType"/> to <see cref="string"/>s
    /// </summary>
    public class StatusDescriptionToStringJsonConverter : BaseEnumJsonConverter<StatusDescriptionType>
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public StatusDescriptionToStringJsonConverter()
        {

        }

        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        protected override IReadOnlyDictionary<StatusDescriptionType, string> GetMapper() => SkroutzLastMileClientConstants.StatusDescriptionTypeToStringMapper;

        #endregion
    }
}
