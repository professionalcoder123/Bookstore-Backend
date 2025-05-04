namespace BookstoreBackend
{
    public class ValidationErrorResponse
    {
        public bool Status { get; set; }
        public List<ValidationError> Errors { get; set; }

        public ValidationErrorResponse()
        {
            Status = true;
            Errors = new List<ValidationError>();
        }
    }
}
