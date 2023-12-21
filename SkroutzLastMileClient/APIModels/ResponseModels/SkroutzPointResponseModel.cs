using Newtonsoft.Json;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// Returns all available lockers for the consumer to choose from.
    /// </summary>
    public class SkroutzPointResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Id"/> property
        /// </summary>
        private string? mId;

        /// <summary>
        /// The member of the <see cref="Type"/> property
        /// </summary>
        private string? mType;

        /// <summary>
        /// The member of the <see cref="Name"/> property
        /// </summary>
        private string? mName;

        /// <summary>
        /// The member of the <see cref="StreetName"/> property
        /// </summary>
        private string? mStreetName;

        /// <summary>
        /// The member of the <see cref="City"/> property
        /// </summary>
        private string? mCity;

        /// <summary>
        /// The member of the <see cref="Region"/> property
        /// </summary>
        private string? mRegion;

        /// <summary>
        /// The member of the <see cref="WorkingHours"/> property
        /// </summary>
        private IEnumerable<AvailabilityShiftResponseModel>? mWorkingHours;

        /// <summary>
        /// The member of the <see cref="Model"/> property
        /// </summary>
        private string? mModel;

        /// <summary>
        /// The member of the <see cref="SwipboxVersion"/> property
        /// </summary>
        private string? mSwipboxVersion;
        #endregion

        #region Public Properties

        /// <summary>
        /// The id
        /// </summary>
        [JsonProperty("id")]
        public string Id
        {
            get => mId ?? string.Empty;
            set => mId = value;
        }

        /// <summary>
        /// The type
        /// </summary>
        [JsonProperty("type")]
        public string Type
        {
            get => mType ?? string.Empty;
            set => mType = value;
        }

        /// <summary>
        /// The name of the locker
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get => mName ?? string.Empty;
            set => mName = value;
        }

        /// <summary>
        /// The locker's street name
        /// </summary>
        [JsonProperty("street_name")]
        public string StreetName
        {
            get => mStreetName ?? string.Empty;
            set => mStreetName = value;
        }

        /// <summary>
        /// The locker's street number
        /// </summary>
        [JsonProperty("street_number")]
        public int StreetNumber { get; set; }

        /// <summary>
        /// The locker's city
        /// </summary>
        [JsonProperty("city")]
        public string City
        {
            get => mCity ?? string.Empty;
            set => mCity = value;
        }

        /// <summary>
        /// The zip code
        /// </summary>
        [JsonProperty("zip")]
        public string ZipCode { get; set; }

        /// <summary>
        /// The region
        /// </summary>
        [JsonProperty("region")]
        public string Region
        {
            get => mRegion ?? string.Empty;
            set => mRegion = value;
        }

        /// <summary>
        /// The working hours
        /// </summary>
        [JsonProperty("working_hours")]
        public IEnumerable<AvailabilityShiftResponseModel> WorkingHours
        {
            get => mWorkingHours ?? Enumerable.Empty<AvailabilityShiftResponseModel>();
            set => mWorkingHours = value;
        }

        /// <summary>
        /// Indicates if the locker is indoors
        /// </summary>
        [JsonProperty("indoor")]
        public bool IsIndoor { get; set; }

        /// <summary>
        /// Longitude in decimal degrees
        /// </summary>
        [JsonProperty("lng")]
        public double Longitude { get; set; }

        /// <summary>
        /// Latitude in decimal degrees
        /// </summary>
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// The locker's model
        /// </summary>
        [JsonProperty("model")]
        public string Model
        {
            get => mModel ?? string.Empty;
            set => mModel = value;
        }

        /// <summary>
        /// Indicates if the locker is active
        /// </summary>
        [JsonProperty("active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Indicates if the locker is obsolete
        /// </summary>
        [JsonProperty("obsolete")]
        public bool IsObsolete { get; set; }

        /// <summary>
        /// The swipbox version
        /// </summary>
        [JsonProperty("swipbox_version")]
        public string SwipboxVersion
        {
            get => mSwipboxVersion ?? string.Empty;
            set => mSwipboxVersion = value;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SkroutzPointResponseModel() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override string ToString() => $"{City} - {Name}";

        #endregion
    }
}