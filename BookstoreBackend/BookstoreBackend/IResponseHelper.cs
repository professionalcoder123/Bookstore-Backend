namespace BookstoreBackend
{
    public interface IResponseHelper
    {
        APIResponse<T> Success<T>(string message, T? data = default);
        APIResponse<T> Failure<T>(string message, T? data = default);
    }
}
