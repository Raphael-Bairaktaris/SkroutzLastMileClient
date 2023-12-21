using Newtonsoft.Json;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// Represents a set of Skroutz points
    /// </summary>
    public class InternalSkroutzPointResponseModel
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="SkroutzPoints"/> property
        /// </summary>
        private IEnumerable<SkroutzPointResponseModel>? mSkroutzPoints;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Skroutz points
        /// </summary>
        [JsonProperty("skroutz_points")]
        public IEnumerable<SkroutzPointResponseModel> SkroutzPoints
        {
            get => mSkroutzPoints ?? Enumerable.Empty<SkroutzPointResponseModel>();
            set => mSkroutzPoints = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public InternalSkroutzPointResponseModel() : base()
        {

        }

        #endregion
    }
}
