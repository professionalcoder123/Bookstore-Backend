namespace BookstoreBackend
{
    public class APIResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public static APIResponse<T> Success(string message, T data)
        {
            return new APIResponse<T> { Status = true, Message = message, Data = data };
        }

        public static APIResponse<T> Fail(string message)
        {
            return new APIResponse<T> { Status = false, Message = message, Data = default };
        }
    }
}
