﻿using Newtonsoft.Json;
using System.Globalization;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// The <see cref="JsonConverter"/> that converts a <see cref="DateTime"/> to <see cref="string"/>
    /// </summary>
    public class DateTimeToStringJsonConverter : JsonConverter<DateTime>
    {
        #region Constants

        /// <summary>
        /// The format that is used for serializing and deserializing <see cref="DateTime"/>s
        /// </summary>
        public const string DateFormat = "yyyy-MM-ddTHH:mm:ss+00:00";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DateTimeToStringJsonConverter() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var readerValue = serializer.Deserialize<string>(reader)!;

            return DateTime.ParseExact(readerValue, DateFormat, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/> 
        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            //value = value.ToUniversalTime();

            var r = value.ToString(DateFormat);

            writer.WriteValue(value.ToString(r));
        }

        #endregion
    }
}