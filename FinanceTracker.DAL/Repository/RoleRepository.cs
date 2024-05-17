using FinanceTracker.DAL.Data;
using FinanceTracker.DAL.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.DAL.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _roleContext;
    public RoleRepository(ApplicationDbContext roleContext)
    {
        _roleContext = roleContext;
    }
    public async Task<List<RoleModel>> GetRoleModel()
    {
        var data = await _roleContext.Role.ToListAsync();
        return data;
    }
}
