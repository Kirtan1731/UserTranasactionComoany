using FinanceTracker.DAL.Data;
using FinanceTracker.DAL.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.DAL;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _UserContext;
    public UserRepository(ApplicationDbContext userContext) 
    {
        _UserContext = userContext;
    }
    public async Task<UserRequestDto> AddUser(UserRequestDto users)
    {
        var data = users.signupDTOMapper();
        await _UserContext.AddAsync(data);
        await _UserContext.SaveChangesAsync();
        return users;
    }
    public async Task<bool> IsEmailTaken(string email)
    {
        return await _UserContext.UserData.AnyAsync(u => u.EmailId == email);
    }
    

}