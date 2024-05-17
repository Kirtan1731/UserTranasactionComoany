using FinanceTracker.DAL.DTOs;
using FinanceTracker.DAL.Model.AuthoritySetting;

namespace FinanceTracker.DAL;

public static class UserDTOmapper
{
    public static UserModel signupDTOMapper(this UserRequestDto users)
    {
        return new UserModel{
            FirstName = users.FirstName,
            LastName = users.LastName,
            EmailId = users.Email,
            PhoneNumber = users.PhoneNumber,
            SubjectId = users.SubjectId
        };
    }
}
