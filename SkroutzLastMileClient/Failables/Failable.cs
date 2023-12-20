using System.Diagnostics.CodeAnalysis;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// The default implementation of the <see cref="IFailable"/> interface
    /// </summary>
    public class Failable : IFailable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Exception"/> property
        /// </summary>
        private Exception? mException;

        #endregion

        #region Public Properties

        /// <summary>
        /// The exception that was thrown
        /// </summary>
        public Exception? Exception
        {
            get => mException ?? (IsSuccessful ? null : new Exception(ErrorMessage));

            set => mException = value;
        }

        /// <summary>
        /// The error type
        /// </summary>
        public virtual ErrorType ErrorType { get; set; } = ErrorType.Error;

        /// <summary>
        /// The error message
        /// </summary>
        public virtual string? ErrorMessage { get; set; }

        /// <summary>
        /// A flag indicating whether the task was successful or not
        /// </summary>
        [MemberNotNullWhen(false, nameof(ErrorMessage), nameof(Exception))]
        public bool IsSuccessful => ErrorMessage is null;

        /// <summary>
        /// The instance of the <see cref="Failable"/> that indicates a successful operation
        /// </summary>
        public static Failable Success { get; } = new SuccessOnlyFailable();

        /// <summary>
        /// Error message indicating that the operation was aborted
        /// </summary>
        public static string OperationAbortedErrorMessage => "Operation aborted!";

        /// <summary>
        /// The instance of the <see cref="Failable"/> that is used for indicating an operation
        /// that was aborted with a hidden error
        /// </summary>
        public static Failable OperationAborted { get; } = new OperationAbortedOnlyFailable();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Failable()
        {

        }

        /// <summary>
        /// Exception based constructor
        /// </summary>
        /// <param name="ex">The exception</param>
        public Failable(Exception ex) : base()
        {
            Exception = ex;
            ErrorMessage = ex.AggregateExceptionMessages();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="Failable"/> from the specified <paramref name="ex"/>
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <returns></returns>
        public static Failable FromException(Exception ex) => new Failable(ex);

        /// <summary>
        /// Creates and returns a <see cref="Failable"/> from the specified <paramref name="errorMessage"/>
        /// </summary>
        /// <param name="errorMessage">The error message</param>
        /// <returns></returns>
        public static Failable FromErrorMessage(string errorMessage) => new Failable() { ErrorMessage = errorMessage };

        /// <summary>
        /// Creates and returns a <see cref="Failable{TResult}"/> that represents the specified <paramref name="result"/>
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <param name="result">The result</param>
        /// <returns></returns>
        public static Failable<T> FromResult<T>(T result) => new Failable<T>(result);

        /// <summary>
        /// Creates and returns a <see cref="Failable{TResult}"/> that represents the specified <paramref name="ex"/>
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <param name="ex">The exception</param>
        /// <returns></returns>
        public static Failable<T> FromException<T>(Exception ex) => new Failable<T>(ex);

        /// <summary>
        /// Creates and returns a <see cref="Failable{TResult}"/> that represents the specified <paramref name="errorMessage"/>
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <param name="errorMessage">The error message</param>
        /// <returns></returns>
        public static Failable<T> FromErrorMessage<T>(string errorMessage) => new Failable<T>() { ErrorMessage = errorMessage };

        /// <summary>
        /// Creates and returns a <see cref="Failable{TResult}"/> that represents an aborted operation
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <returns></returns>
        public static Failable<T> FromAbortedOperation<T>() => Failable<T>.OperationAborted;

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => IFailableHelpers.GetStringRepresentation(this);

        #endregion

        #region Operators

        /// <summary>
        /// Creates a <see cref="Failable"/> using the specified string as its error message
        /// </summary>
        /// <param name="s">The string</param>
        public static implicit operator Failable(string s) => new Failable() { ErrorMessage = s };

        /// <summary>
        /// Creates a <see cref="Failable"/> using the message of the specified ex and its inner exceptions
        /// as its error message
        /// </summary>
        /// <param name="ex">The exception</param>
        public static implicit operator Failable(Exception ex) => new Failable(ex);

        #endregion
    }

    /// <summary>
    /// An internal implementation of the <see cref="Failable"/> that always has
    /// a successful state
    /// </summary>
    internal class SuccessOnlyFailable : Failable
    {
        #region Public Properties

        /// <summary>
        /// The error message
        /// </summary>
        public override string? ErrorMessage { get => null; set { } }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SuccessOnlyFailable() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// An internal implementation of the <see cref="Failable"/> that is used for 
    /// indicating an aborted operation
    /// </summary>
    internal class OperationAbortedOnlyFailable : Failable
    {
        #region Public Properties

        /// <summary>
        /// The error message
        /// </summary>
        public override string? ErrorMessage { get => Failable.OperationAbortedErrorMessage; set { } }

        /// <summary>
        /// The error type
        /// </summary>
        public override ErrorType ErrorType { get => ErrorType.Hidden; set { } }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public OperationAbortedOnlyFailable() : base()
        {

        }

        #endregion
    }

    /// <summary>
    /// The default implementation of the <see cref="IFailable{TResult}"/> interface
    /// </summary>
    /// <typeparam name="TResult">The type of the result</typeparam>
    public class Failable<TResult> : Failable, IFailable<TResult>
    {
        #region Constants

        /// <summary>
        /// The error message for the exception
        /// </summary>
        public const string ExceptionMessage = $"The flag {nameof(IsSuccessful)} has the be true in order to access the {nameof(Result)}.";

        #endregion

        #region Private Methods

        /// <summary>
        /// The member of the <see cref="Result"/>
        /// </summary>
        private TResult? mResult;

        #endregion

        #region Public Properties

        /// <summary>
        /// The result
        /// </summary>
        public TResult Result
        {
            get => IsSuccessful ? mResult! : throw new InvalidOperationException($"{ExceptionMessage} Error: {ErrorMessage}");

            set => mResult = value;
        }

        /// <summary>
        /// The instance of the <see cref="Failable{TResult}"/> that is used for indicating an operation
        /// that was aborted with a hidden error
        /// </summary>
        public new static Failable<TResult> OperationAborted { get; } = new OperationAbortedOnlyFailable<TResult>();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Failable() : base()
        {

        }

        /// <summary>
        /// Result based constructor
        /// </summary>
        /// <param name="result">The result</param>
        public Failable(TResult result)
        {
            Result = result;
        }

        /// <summary>
        /// Exception based constructor
        /// </summary>
        /// <param name="ex">The exception</param>
        public Failable(Exception ex) : base(ex)
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => IFailableHelpers.GetStringRepresentation(this);

        #endregion

        #region Operators

        /// <summary>
        /// Creates a <see cref="Failable{TResult}"/> using the specified string as its error message
        /// </summary>
        /// <param name="s">The string</param>
        public static implicit operator Failable<TResult>(string s) => new Failable<TResult>(new Exception(s));

        /// <summary>
        /// Creates a <see cref="Failable{TResult}"/> using the message of the specified ex and its inner exceptions
        /// as its error message
        /// </summary>
        /// <param name="ex">The exception</param>
        public static implicit operator Failable<TResult>(Exception ex) => new Failable<TResult>(ex);

        /// <summary>
        /// Creates a <see cref="Failable{TResult}"/> using the specified result as its result
        /// </summary>
        /// <param name="result">The result</param>
        public static implicit operator Failable<TResult>(TResult result) => new Failable<TResult>(result);

        #endregion
    }

    /// <summary>
    /// An internal implementation of the <see cref="Failable{TResult}"/> that is used for 
    /// indicating an aborted operation
    /// </summary>
    internal class OperationAbortedOnlyFailable<TResult> : Failable<TResult>
    {
        #region Public Properties

        /// <summary>
        /// The error message
        /// </summary>
        public override string? ErrorMessage { get => "Operation aborted!"; set { } }

        /// <summary>
        /// The error type
        /// </summary>
        public override ErrorType ErrorType { get => ErrorType.Hidden; set { } }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public OperationAbortedOnlyFailable() : base()
        {

        }

        #endregion
    }
}
