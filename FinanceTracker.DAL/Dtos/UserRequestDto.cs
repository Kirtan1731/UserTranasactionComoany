using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinanceTracker.DAL.Resources;

namespace FinanceTracker.DAL.DTOs
{
    public class UserRequestDto
    {
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = ValidationResources.FirstNameRegexMessage)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = ValidationResources.FirstNameLengthMessage)]
        public string FirstName { get; set; }
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = ValidationResources.LastNameRegexMessage)]
        [StringLength(30, ErrorMessage = ValidationResources.LastNameLengthMessage)]
        public string? LastName { get; set; }
        [RegularExpression(@"^[a-zA-Z][\w\.-]*@(?:[a-zA-Z\d-]+\.)+(?:com|in)$", ErrorMessage = ValidationResources.InvalidEmail)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = ValidationResources.EmailLength)]
        public string Email { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = ValidationResources.InvalidPhone)]
        public string PhoneNumber { get; set; }
        [StringLength(25, MinimumLength = 8, ErrorMessage = ValidationResources.PasswordLength)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = ValidationResources.InvalidPassword)]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public string? SubjectId { get; set; }

    }
}