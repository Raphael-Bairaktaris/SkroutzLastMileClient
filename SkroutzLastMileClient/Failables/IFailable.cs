using System.Diagnostics.CodeAnalysis;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// Provides abstractions for an object that can fail its task
    /// </summary>
    public interface IFailable
    {
        #region Properties

        /// <summary>
        /// The instance of the <see cref="IFailable"/> that indicates a successful operation
        /// </summary>
        public static IFailable Success => Failable.Success;

        /// <summary>
        /// The instance of the <see cref="IFailable"/> that is used for indicating an operation
        /// that was aborted with a hidden error
        /// </summary>
        public static IFailable OperationAborted => Failable.OperationAborted;

        /// <summary>
        /// The exception that was thrown
        /// </summary>
        Exception? Exception { get; }

        /// <summary>
        /// The error type
        /// </summary>
        ErrorType ErrorType { get; }

        /// <summary>
        /// The error message
        /// </summary>
        string? ErrorMessage { get; }

        /// <summary>
        /// A flag indicating whether the task was successful or not
        /// </summary>
        [MemberNotNullWhen(false, nameof(ErrorMessage), nameof(Exception))]
        bool IsSuccessful { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates and returns a <see cref="IFailable"/> from the specified <paramref name="ex"/>
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <returns></returns>
        public static IFailable FromException(Exception ex) => Failable.FromException(ex);

        /// <summary>
        /// Creates and returns a <see cref="IFailable"/> from the specified <paramref name="errorMessage"/>
        /// </summary>
        /// <param name="errorMessage">The error message</param>
        /// <returns></returns>
        public static IFailable FromErrorMessage(string errorMessage) => Failable.FromErrorMessage(errorMessage);

        /// <summary>
        /// Creates and returns a <see cref="IFailable{T}"/> that represents the specified <paramref name="result"/>
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <param name="result">The result</param>
        /// <returns></returns>
        public static IFailable<T> FromResult<T>(T result) => Failable.FromResult(result);

        /// <summary>
        /// Creates and returns a <see cref="IFailable{TResult}"/> that represents the specified <paramref name="ex"/>
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <param name="ex">The exception</param>
        /// <returns></returns>
        public static IFailable<T> FromException<T>(Exception ex) => Failable.FromException<T>(ex);

        /// <summary>
        /// Creates and returns a <see cref="Failable{TResult}"/> that represents the specified <paramref name="errorMessage"/>
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <param name="errorMessage">The error message</param>
        /// <returns></returns>
        public static IFailable<T> FromErrorMessage<T>(string errorMessage) => Failable.FromErrorMessage<T>(errorMessage);

        /// <summary>
        /// Creates and returns a <see cref="IFailable{TResult}"/> that represents an aborted operation
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <returns></returns>
        public static IFailable<T> FromAbortedOperation<T>() => Failable<T>.OperationAborted;

        #endregion
    }

    /// <summary>
    /// A <see cref="IFailable"/> that contains a result
    /// </summary>
    /// <typeparam name="TResult">The type of the result</typeparam>
    public interface IFailable<out TResult> : IFailable
    {
        #region Properties

        /// <summary>
        /// The instance of the <see cref="IFailable{TResult}"/> that is used for indicating an operation
        /// that was aborted with a hidden error
        /// </summary>
        public new static IFailable<TResult> OperationAborted { get; } = new OperationAbortedOnlyFailable<TResult>();

        /// <summary>
        /// The result
        /// </summary>
        TResult Result { get; }

        #endregion
    }
}
