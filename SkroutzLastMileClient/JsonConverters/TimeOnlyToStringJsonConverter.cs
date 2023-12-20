using Newtonsoft.Json;
using System.Globalization;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// The <see cref="JsonConverter"/> that converts a <see cref="TimeOnly"/> to <see cref="string"/>
    /// </summary>
    public class TimeOnlyToStringJsonConverter : JsonConverter<TimeOnly>
    {
        #region Constants

        /// <summary>
        /// The format that is used for serializing and deserializing <see cref="TimeOnly"/>s
        /// </summary>
        public const string TimeFormat = "HH:mm";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TimeOnlyToStringJsonConverter() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override TimeOnly ReadJson(JsonReader reader, Type objectType, TimeOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var readerValue = serializer.Deserialize<string>(reader)!;

            if (readerValue == "24:00")
                return default;

            return TimeOnly.ParseExact(readerValue, TimeFormat, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/> 
        public override void WriteJson(JsonWriter writer, TimeOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(TimeFormat));
        }

        #endregion
    }
}