namespace FinanceTracker.DAL.Dtos
{
    public class ResponseDto
    {
        public object? Data { get; set; }
        public string? Error { get; set; }
        public int TotalRecordCount { get; set; }
    }
}