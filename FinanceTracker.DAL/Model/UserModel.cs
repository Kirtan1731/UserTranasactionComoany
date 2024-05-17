using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL;

public class UserModel
{
    [Key]
    public int? UserId { get; set;}

    [Required(ErrorMessage = "This field is required.")]
    public string FirstName { get; set; }
    public string? LastName { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    public string EmailId { get; set; }
    public string? PhoneNumber { get; set; }
    public byte? UserStatusId { get; set; } = 1;
    public string? RejectionReason { get; set; }
    public byte RoleId { get; set; } = 2;
     public bool IsActive { get; set; } = false;
    public DateTime CreatedDate { get; set; } =  DateTime.Now;
    public int CreatedBy { get; set; } = 1;
    public DateTime? ModifiedDate { get; set; }
    public int? ModifiedBy { get; set; }
    public string SubjectId { get; set; } 
    public DateTime? DeletedDate { get; set; }
    public int? DeletedBy { get; set; }

    public byte? GenderId { get; set; }
    public string? Address { get; set; }
    public byte? CountryId { get; set; }
    public byte? StateId { get; set; }
    public int? CityId { get; set; }
    public string?  Pincode { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? ActualImageName { get; set; }
    public string? ImageName { get; set; }
    public DateTime? StatusTimestamp { get; set; } 
}
