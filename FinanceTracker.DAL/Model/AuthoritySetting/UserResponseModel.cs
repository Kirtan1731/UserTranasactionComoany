namespace FinanceTracker.DAL.Model.AuthoritySetting
{
    public class UserResponseModel
    {
        public int StatusCode { get; set; } 
        public string? SubjectId  { get; set;}
        public string? ResponseStatus { get; set; } 
        public List<string>? ErrorMessage { get; set; } 
        public List<string>? ErrorException { get; set; } 
        public string? Token { get; set; }
        public string? Username { get; set; }
         public bool IsSuccessful { get; set; }
        public string? Content { get; set;}
    }
}