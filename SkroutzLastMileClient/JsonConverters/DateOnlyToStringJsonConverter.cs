using Newtonsoft.Json;
using System.Globalization;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// The <see cref="JsonConverter"/> that converts a <see cref="DateOnly"/> to <see cref="string"/>
    /// </summary>
    public class DateOnlyToStringJsonConverter : JsonConverter<DateOnly>
    {
        #region Constants

        /// <summary>
        /// The format that is used for serializing and deserializing <see cref="DateOnly"/>s
        /// </summary>
        public const string DateFormat = "yyyy-MM-dd";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DateOnlyToStringJsonConverter() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var readerValue = serializer.Deserialize<string>(reader)!;

            return DateOnly.ParseExact(readerValue, DateFormat, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/> 
        public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(DateFormat));
        }

        #endregion
    }
}