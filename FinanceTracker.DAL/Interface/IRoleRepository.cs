using FinanceTracker.DAL.Model;

namespace FinanceTracker.DAL.Interface;

public interface IRoleRepository
{
    Task<List<RoleModel>> GetRoleModel();
}
