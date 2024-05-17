using FinanceTracker.BLL.Interface;
using FinanceTracker.DAL;
using FinanceTracker.DAL.Interface;

namespace FinanceTracker.BLL.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<List<RoleModel>> GetRoles()
    {
        return await _roleRepository.GetRoleModel();
    }
}
