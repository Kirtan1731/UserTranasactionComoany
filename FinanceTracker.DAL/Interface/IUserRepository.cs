using FinanceTracker.DAL.DTOs;

namespace FinanceTracker.DAL;

public interface IUserRepository
{
    Task<UserRequestDto> AddUser(UserRequestDto users);
    Task<bool> IsEmailTaken(string email);
    
}
