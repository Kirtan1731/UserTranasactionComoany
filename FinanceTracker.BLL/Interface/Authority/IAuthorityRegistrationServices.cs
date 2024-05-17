using FinanceTracker.DAL;
using FinanceTracker.DAL.Model.AuthoritySetting;

namespace FinanceTracker.BLL
{
    public interface IAuthorityRegistrationServices
    {
        Task<ApiResponse<UserResponseModel>> RegisterUser(AuthorityRegistrationDto authorityRegisterModel);
        Task<ApiResponse<UserResponseModel>> UpdateApprove(string subjectId);
    }
}