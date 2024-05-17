using FinanceTracker.DAL.Dtos;
using FinanceTracker.DAL.DTOs;

namespace FinanceTracker.DAL;

public interface IUserServices
{
    Task<bool> IsEmailTakenCheck(string email);
    Task<RoleResponseDto> RegisterUserWithAuthority(UserRequestDto userRequest);
}
