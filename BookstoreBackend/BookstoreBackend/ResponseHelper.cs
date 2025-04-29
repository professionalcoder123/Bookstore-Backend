namespace BookstoreBackend
{
    public class ResponseHelper : IResponseHelper
    {
        public APIResponse<T> Failure<T>(string message, T? data = default)
        {
            return new APIResponse<T>
            {
                Status = false,
                Message = message,
                Data = data
            };
        }

        public APIResponse<T> Success<T>(string message, T? data = default)
        {
            return new APIResponse<T>
            {
                Status = true,
                Message = message,
                Data = data
            };
        }
    }
}