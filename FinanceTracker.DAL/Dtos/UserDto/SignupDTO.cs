using System.ComponentModel.DataAnnotations;
using FinanceTracker.DAL.Resources;

namespace FinanceTracker.DAL.DTOs;

public class SignupDTO
{
    private string _email;
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = ValidationResources.FirstNameRegexMessage )]
    [StringLength(30, MinimumLength = 2, ErrorMessage = ValidationResources.FirstNameLengthMessage)]
    public string FirstName { get; set; }

    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = ValidationResources.LastNameRegexMessage)]
    [StringLength(30, MinimumLength = 2, ErrorMessage = ValidationResources.LastNameLengthMessage)]
    public string? LastName { get; set; }


    [RegularExpression(@"^[a-zA-Z][\w\.-]*@(?:[a-zA-Z\d-]+\.)+(?:com|in)$", ErrorMessage = ValidationResources.InvalidEmail)]
    [StringLength(50, MinimumLength = 1, ErrorMessage = ValidationResources.EmailLength)]
    public string EmailId { get { return _email; } set { _email = value?.ToLower(); } }

    [RegularExpression(@"^\d{10}$", ErrorMessage =ValidationResources.InvalidPhone )]
    public string? PhoneNumber { get; set; }
}
