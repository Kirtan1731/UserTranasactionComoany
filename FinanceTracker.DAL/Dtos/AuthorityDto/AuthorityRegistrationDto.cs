using FinanceTracker.DAL.DTOs;
using FinanceTracker.DAL.Resources;

namespace FinanceTracker.DAL
{
    public class AuthorityRegistrationDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
   
        public string  UserName {get;set;}
      
        public string PasswordHash { get; set; }
        public string UserClient { get; set; } = ValidationResources.UserClientName;
        public bool IsActive { get; set; } 
        public List<ClaimsDto> Claims { get; set; }
        public List<RoleDto> Roles { get; set; } 
        public string SubjectId { get; set; }
    }
}