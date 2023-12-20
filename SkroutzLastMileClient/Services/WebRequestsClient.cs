using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace SkroutzLastMileClient
{
    /// <summary>
    /// Client that provides HTTP calls for sending and receiving information from a HTTP server
    /// </summary>
    /// <typeparam name="TAuthenticationArgs">
    /// The type of the authentication args.
    /// NOTE: The authentication args determine the authentication scheme that will be used against the server!
    /// </typeparam>
    public abstract class WebRequestsClient<TAuthenticationArgs>
        where TAuthenticationArgs : class
    {
        #region Constants

        /// <summary>
        /// The name of the PATCH HTTP method
        /// </summary>
        public const string PATCH = "PATCH";

        /// <summary>
        /// The json request and response preferred media type
        /// </summary>
        public const string MediaTypeJson = "application/json";

        /// <summary>
        /// The standard deserialization error message
        /// </summary>
        public string DeserializationErrorMessage { get; set; } = "Failed to deserialize server response to the expected type!";

        #endregion

        #region Private Members

        /// <summary>
        /// THe member of the <see cref="Client"/> property
        /// </summary>
        private HttpClient? mClient;

        #endregion

        #region Protected Properties

        /// <summary>
        /// The HTTP client that is used for sending the HTTP requests
        /// NOTE: HTTP client is recommended to be injected as a Singleton in DI
        ///       https://stackoverflow.com/questions/4015324/how-to-make-http-post-web-request
        /// </summary>
        protected HttpClient Client
        {
            get
            {
                if (mClient is null)
                {
                    mClient = CreateClient();

                    ConfigureClient(mClient);
                }

                return mClient;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WebRequestsClient() : base()
        {

        }

        #endregion

        #region Public Methods

        #region Non-Generic

        #region POST

        /// <summary>
        /// POSTs a web request to an URL that contains the specified <paramref name="content"/> and returns a <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to post</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<HttpResponseMessage> PostAsync(string url, object? content, TAuthenticationArgs? authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            HttpResponseMessage responseMessage;

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"POST request made to: {url}");
#endif

            try
            {
                // If there is content...
                if (content is not null)
                    responseMessage = await Client.PostAsync(url, CreateStringContent(content));
                // Else...
                else
                    responseMessage = await Client.PostAsync(url, null);
            }
            catch
            {
                // Re-throw
                throw;
            }

            #endregion

            return responseMessage;
        }

        /// <summary>
        /// POSTs a web request to an URL that contains the specified <paramref name="content"/> and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to post</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<byte[]>> PostBytesAsync(string url, object content, TAuthenticationArgs? authenticationArgs)
        {
#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"POST bytes request made to: {url}");
#endif

            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard Post call first
                serverResponse = await PostAsync(url, content, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<byte[]>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultFromStreamAsync();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result.RawServerResponse);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #region GET

        /// <summary>
        /// GETs a web request to a URL and returns a <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<HttpResponseMessage> GetAsync(string url, TAuthenticationArgs? authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"GET request made to: {url}");
#endif

            try
            {
                // Send it with the request
                return await Client.GetAsync(url);
            }
            catch
            {
                // Re-throw
                throw;
            }

            #endregion
        }

        /// <summary>
        /// GETs the bytes that are returned in the <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<IFailable<byte[]>> GetBytesAsync(string url, TAuthenticationArgs? authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"GET bytes request made to: {url}");
#endif

            try
            {
                // Send it with the request
                var response = await Client.GetAsync(url);

                // Make sure we got a valid response
                response.EnsureSuccessStatusCode();

                // Load the content
                await response.Content.LoadIntoBufferAsync();

                // Return
                return Failable.FromResult(await response.Content.ReadAsByteArrayAsync());

            }
            catch (Exception ex)
            {
                return new Failable<byte[]>(ex);
            }

            #endregion
        }

        /// <summary>
        /// Downloads the file from the specified <paramref name="url"/> and saves it at the specified <paramref name="fileName"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="fileName">The file path combined with the file name</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<IFailable> DownloadFileAsync(string url, string fileName, TAuthenticationArgs authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"GET file (download) request made to: {url}");
#endif

            try
            {
                // Send it with the request
                var response = await Client.GetStreamAsync(url);

                using (var fs = new FileStream(fileName, FileMode.CreateNew))
                {
                    await response.CopyToAsync(fs);
                }

                return Failable.Success;
            }
            catch (Exception ex)
            {
                return new Failable(ex);
            }

            #endregion
        }

        #endregion

        #region PUT

        /// <summary>
        /// PUTs the specified <paramref name="content"/> at the specified <paramref name="url"/> and returns a <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to put</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<HttpResponseMessage> PutAsync(string url, object? content, TAuthenticationArgs? authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"PUT request made to: {url}");
#endif

            try
            {
                // Send it with the request
                return await Client.PutAsync(url, content is null ? null : CreateStringContent(content));
            }
            catch
            {
                // Re-throw
                throw;
            }

            #endregion
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Sends a DELETE request and returns a <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<HttpResponseMessage> DeleteAsync(string url, TAuthenticationArgs? authenticationArgs)
        {
            // Set the authorization scheme
            SetAuthorizationHeader(authenticationArgs);

            #region Send Request

#if DEBUG
            if (ShouldLogRequests())
                Debug.WriteLine($"DELETE request made to: {url}");
#endif

            try
            {
                // Send it with the request
                return await Client.DeleteAsync(url);
            }
            catch
            {
                // Re-throw
                throw;
            }

            #endregion
        }

        #endregion

        #endregion

        #region Generic

        #region POST

        /// <summary>
        /// POSTs a web request to an URL that contains the specified <paramref name="content"/> and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <typeparam name="TResponse">The type of the response</typeparam>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to post</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<TResponse>> PostAsync<TResponse>(string url, object? content, TAuthenticationArgs? authenticationArgs)
        {
            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard Post call first
                serverResponse = await PostAsync(url, content, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<TResponse>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultAsync<TResponse>();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result);

                // Done
                return result;
            }

            // If we have no content to deserialize...
            if (result.RawServerResponse.IsNullOrEmpty())
                // Done
                return result;

            // Deserialize raw response
            try
            {
                // Deserialize Json string
                result.Result = Deserialize<TResponse>(result.RawServerResponse);
            }
            catch (Exception ex)
            {
                // Break
                Debugger.Break();

                // If deserialize failed then set error message
                result.ErrorMessage = ParseErrorMessageCore(result, typeof(TResponse), ex);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #region GET

        /// <summary>
        /// GETs a web request to a URL and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<TResponse>> GetAsync<TResponse>(string url, TAuthenticationArgs? authenticationArgs)
        {
            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard GET call first
                serverResponse = await GetAsync(url, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<TResponse>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultAsync<TResponse>();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result);

                // Done
                return result;
            }

            // If we have no content to deserialize...
            if (result.RawServerResponse.IsNullOrEmpty())
                // Done
                return result;

            // Deserialize raw response
            try
            {
                // Deserialize Json string
                result.Result = Deserialize<TResponse>(result.RawServerResponse);
            }
            catch (Exception ex)
            {
                // Break
                Debugger.Break();

                // If deserialize failed then set error message
                result.ErrorMessage = ParseErrorMessageCore(result, typeof(TResponse), ex);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #region PUT

        /// <summary>
        /// PUTs the specified <paramref name="content"/> at the specified <paramref name="url"/> and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="content">The content to put</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<TResponse>> PutAsync<TResponse>(string url, object? content, TAuthenticationArgs? authenticationArgs)
        {
            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard PUT call first
                serverResponse = await PutAsync(url, content, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<TResponse>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultAsync<TResponse>();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result);

                // Done
                return result;
            }

            // If we have no content to deserialize...
            if (result.RawServerResponse.IsNullOrEmpty())
                // Done
                return result;

            // Deserialize raw response
            try
            {
                // Deserialize Json string
                result.Result = Deserialize<TResponse>(result.RawServerResponse);
            }
            catch (Exception ex)
            {
                // Break
                Debugger.Break();

                // If deserialize failed then set error message
                result.ErrorMessage = ParseErrorMessageCore(result, typeof(TResponse), ex);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Sends a DELETE request and returns a <see cref="WebRequestResult{T}"/>
        /// </summary>
        /// <param name="url">The URL</param>
        /// <param name="authenticationArgs">If specified, provides the Authorization header is set</param>
        /// <returns></returns>
        public async Task<WebRequestResult<TResponse>> DeleteAsync<TResponse>(string url, TAuthenticationArgs? authenticationArgs)
        {
            // Create server response holder
            HttpResponseMessage serverResponse;
            try
            {
                // Make the standard DELETE call first
                serverResponse = await DeleteAsync(url, authenticationArgs);
            }
            catch (Exception ex)
            {
                // If we got unexpected error, return that
                return new WebRequestResult<TResponse>(ex);
            }

            // Create the result
            var result = await serverResponse.CreateWebRequestResultAsync<TResponse>();

            // If the response status code is not 200...
            if (!serverResponse.IsSuccessStatusCode)
            {
                // Call failed
                result.ErrorMessage = ParseErrorMessageCore(result);

                // Done
                return result;
            }

            // If we have no content to deserialize...
            if (result.RawServerResponse.IsNullOrEmpty())
                // Done
                return result;

            // Deserialize raw response
            try
            {
                // Deserialize Json string
                result.Result = Deserialize<TResponse>(result.RawServerResponse);
            }
            catch (Exception ex)
            {
                // Break
                Debugger.Break();

                // If deserialize failed then set error message
                result.ErrorMessage = ParseErrorMessageCore(result, typeof(TResponse), ex);

                // Done
                return result;
            }

            // Return result
            return result;
        }

        #endregion

        #endregion

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets a flag indicating whether the requests should be logged or not
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldLogRequests() => true;

        /// <summary>
        /// Creates and returns an <see cref="HttpClient"/>
        /// </summary>
        /// <returns></returns>
        protected virtual HttpClient CreateClient() => new HttpClient();

        /// <summary>
        /// Configures the specified <paramref name="client"/>
        /// </summary>
        /// <param name="client">The client</param>
        protected virtual void ConfigureClient(HttpClient client) => client.Timeout = new TimeSpan(0, 30, 0);

        /// <summary>
        /// Configures the specified <paramref name="stringContent"/>
        /// </summary>
        /// <param name="stringContent">The content</param>
        protected virtual void ConfigureStringContent(StringContent stringContent) { }

        /// <summary>
        /// Serializes the specified <paramref name="obj"/> to a string
        /// before sending the request
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns></returns>
        protected virtual string Serialize(object obj) => JsonConvert.SerializeObject(obj);

        /// <summary>
        /// Deserializes the specified <paramref name="rawServerResponse"/> to the requested type
        /// </summary>
        /// <typeparam name="TResponse">The type to deserialize to</typeparam>
        /// <param name="rawServerResponse">The raw server response</param>
        /// <returns></returns>
        protected virtual TResponse Deserialize<TResponse>(string rawServerResponse)
        {
            // If the response is a string....
            if (typeof(TResponse) == typeof(string))
                return (TResponse)(object)rawServerResponse;

            // Deserialize the JSON
            var result = JsonConvert.DeserializeObject<TResponse>(rawServerResponse);

            // If the deserialization failed...
            if (result is null)
                throw new InvalidOperationException("Invalid response format");

            // Return the result
            return result;
        }

        /// <summary>
        /// Gets the media type header that is attached to the request before sending it
        /// </summary>
        /// <returns></returns>
        protected virtual string GetMediaType() => MediaTypeJson;

        /// <summary>
        /// Gets the encoding that is attached to the request before sending it
        /// </summary>
        /// <returns></returns>
        protected virtual Encoding GetEncoding() => Encoding.UTF8;

        /// <summary>
        /// Creates and returns a <see cref="AuthenticationHeaderValue"/> using the
        /// specified <paramref name="authenticationArgs"/>
        /// </summary>
        /// <param name="authenticationArgs">The authentication arguments</param>
        /// <returns></returns>
        protected abstract AuthenticationHeaderValue CreateAuthenticationHeader(TAuthenticationArgs authenticationArgs);

        /// <summary>
        /// Attempts to parse the error message contained in the <paramref name="result"/>
        /// </summary>
        /// <param name="result">The result</param>
        /// <returns></returns>
        protected virtual string? ParseErrorMessage(WebRequestResult result)
            => result.RawServerResponse;

        /// <summary>
        /// Attempts to parse the error message contained in the specified <paramref name="deserializationException"/>
        /// that was thrown when attempting to deserialize the <paramref name="result"/> to the specified <paramref name="deserializationType"/>
        /// </summary>
        /// <param name="deserializationType">The type that response should have been deserialized to</param>
        /// <param name="deserializationException">The deserialization exception that was thrown</param>
        /// <param name="result">The result</param>
        /// <returns></returns>
        protected virtual string? ParseDeserializationErrorMessage(WebRequestResult result, Type deserializationType, Exception deserializationException)
            => DeserializationErrorMessage;

        /// <summary>
        /// Handles the null arguments
        /// </summary>
        protected virtual void HandleNullAuthenticationArgs() => Client.DefaultRequestHeaders.Authorization = null;

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates and returns the <see cref="StringContent"/> that represents
        /// the specified <paramref name="content"/>
        /// </summary>
        /// <param name="content">The content</param>
        /// <returns></returns>
        private StringContent CreateStringContent(object content)
        {
            // Create the HTTP content
            var httpContent = new StringContent(Serialize(content), GetEncoding(), GetMediaType());

            ConfigureStringContent(httpContent);

            // Return it
            return httpContent;
        }

        /// <summary>
        /// Attempts to parse the error message contained in the <paramref name="response"/>
        /// </summary>
        /// <param name="response">The Web request result</param>
        /// <returns></returns>
        private string? ParseErrorMessageCore(WebRequestResult response)
        {
            try
            {
                return ParseErrorMessage(response);
            }
            catch
            {
                return DeserializationErrorMessage;
            }
        }

        /// <summary>
        /// Attempts to parse the error message contained in the specified <paramref name="deserializationException"/>
        /// that was thrown when attempting to deserialize the <paramref name="response"/> to the specified <paramref name="deserializationType"/>
        /// </summary>
        /// <param name="deserializationType">The type that response should have been deserialized to</param>
        /// <param name="deserializationException">The deserialization exception that was thrown</param>
        /// <param name="response">The Web request result</param>
        /// <returns></returns>
        private string? ParseErrorMessageCore(WebRequestResult response, Type deserializationType, Exception deserializationException)
        {
            try
            {
                return ParseDeserializationErrorMessage(response, deserializationType, deserializationException);
            }
            catch
            {
                return DeserializationErrorMessage;
            }
        }

        /// <summary>
        /// Sets the specified <paramref name="authentication"/> if any to the <see cref="Client"/>
        /// </summary>
        /// <param name="authentication">The token</param>
        private void SetAuthorizationHeader(TAuthenticationArgs? authentication)
        {
            // If there is a bearer token...
            if (authentication is not null)
                // Set it
                Client.DefaultRequestHeaders.Authorization = CreateAuthenticationHeader(authentication);
            else
                // Handle the null arguments
                HandleNullAuthenticationArgs();
        }

        #endregion
    }

    /// <summary>
    /// The JWT token based implementation of the <see cref="WebRequestsClient{TAuthenticationArgs}"/>
    /// </summary>
    public class WebRequestsClient : WebRequestsClient<string>
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Instance"/>
        /// </summary>
        private static Lazy<WebRequestsClient> mInstance = new(() => new());

        #endregion

        #region Public Properties

        #region Singleton

        /// <summary>
        /// A single instance of the <see cref="WebRequestsClient"/>
        /// </summary>
        public static WebRequestsClient Instance => mInstance.Value;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected WebRequestsClient() : base()
        {

        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Creates and returns a <see cref="AuthenticationHeaderValue"/> using the
        /// specified <paramref name="authenticationArgs"/>
        /// </summary>
        /// <param name="authenticationArgs">The authentication arguments</param>
        /// <returns></returns>
        protected override AuthenticationHeaderValue CreateAuthenticationHeader(string authenticationArgs)
            => new AuthenticationHeaderValue("Bearer", authenticationArgs);

        #endregion
    }
}
