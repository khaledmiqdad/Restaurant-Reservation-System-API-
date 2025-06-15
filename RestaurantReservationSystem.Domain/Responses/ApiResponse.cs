namespace RestaurantReservationSystem.Domain.Responses
{
    /// <summary>
    /// Represents a standardized API response wrapper.
    /// </summary>
    /// <typeparam name="T">The type of the data returned in the response.</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indicates whether the API operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// A message providing additional information about the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The data returned by the API, if applicable.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Creates an empty API response.
        /// </summary>
        public ApiResponse() { }

        /// <summary>
        /// Creates a successful API response with the specified data and optional message.
        /// </summary>
        /// <param name="data">The data to include in the response.</param>
        /// <param name="message">An optional success message.</param>
        public ApiResponse(T data, string message = null)
        {
            Success = true;
            Data = data;
            Message = message ?? "Request completed successfully.";
        }

        /// <summary>
        /// Creates a failed API response with the specified error message.
        /// </summary>
        /// <param name="message">The error message to include.</param>
        public ApiResponse(string message)
        {
            Success = false;
            Message = message;
            Data = default;
        }

        /// <summary>
        /// Creates a successful API response.
        /// </summary>
        /// <param name="data">The data to include in the response.</param>
        /// <param name="message">An optional message.</param>
        /// <returns>An instance of <see cref="ApiResponse{T}"/> representing success.</returns>
        public static ApiResponse<T> SuccessResponse(T data, string message = null)
        {
            return new ApiResponse<T>(data, message);
        }

        /// <summary>
        /// Creates a failed API response.
        /// </summary>
        /// <param name="message">The error message to include.</param>
        /// <returns>An instance of <see cref="ApiResponse{T}"/> representing failure.</returns>
        public static ApiResponse<T> FailResponse(string message)
        {
            return new ApiResponse<T>(message);
        }
    }
}