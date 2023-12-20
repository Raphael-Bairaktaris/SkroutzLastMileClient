using System.Collections;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// Helper methods for <see cref="IFailable"/>
    /// </summary>
    public static class IFailableHelpers
    {
        /// <summary>
        /// Gets the string representation of the specified <paramref name="failable"/>
        /// </summary>
        /// <param name="failable">The failable</param>
        /// <returns></returns>
        public static string GetStringRepresentation(IFailable failable)
        {
            // If there was an error...
            if (!failable.IsSuccessful)
                // Return the error message
                return failable.ErrorMessage;

            // Return successful
            return "Successful";
        }

        /// <summary>
        /// Gets the string representation of the specified <paramref name="failable"/>
        /// </summary>
        /// <param name="failable">The failable</param>
        /// <returns></returns>
        public static string GetStringRepresentation<TResult>(IFailable<TResult> failable)
        {
            // If there was an error...
            if (!failable.IsSuccessful)
                // Return the error message
                return failable.ErrorMessage;

            // If there isn't a result...
            if (failable.Result is null)
                // Return
                return "No result";

            // If the result is an enumerable...
            if (typeof(TResult).IsAssignableTo(typeof(IEnumerable)) && typeof(TResult) != typeof(string))
            {
                // Get the enumerable
                var enumerable = ((IEnumerable)failable.Result).Cast<object>();

                // Get the number of the items of the enumerable
                var enumerableItemsCount = enumerable.Count();

                // If there are more than 10 items...
                if (enumerableItemsCount > 10)
                    // Return the number of the items
                    return $"{enumerableItemsCount} results";
                // Else...
                else
                    // Return the number of the items as well as the string representation of the items
                    return $"{enumerableItemsCount} results ({enumerable.AggregateString(") (")})";
            }

            // Return the string representation of the results
            return failable.Result.ToString() ?? string.Empty;
        }
    }
}
