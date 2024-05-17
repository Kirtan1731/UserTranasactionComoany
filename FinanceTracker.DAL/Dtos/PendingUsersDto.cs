namespace FinanceTracker.DAL.Dtos
{
    public class PendingUsersDto
    {
        public int? UserId { get; set;}
        public string? FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string? EmailId { get; set; }
        public string? PhoneNumber { get; set; }
    }
}