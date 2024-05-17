namespace FinanceTracker
{
    public class ApiResponse<dynamic>
    {
        public string? Message { get; set; }
        public List<string> Error { get; set; }
        public dynamic? Data { get; set; }
        public int StatusCode { get; set; } 
        string? SubjectId { get; set; }

        public ApiResponse()
        {
            Data = default;
            Message = null;
            Error = null;
        }

    }
}