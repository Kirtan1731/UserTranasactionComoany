using FinanceTracker.DAL;

namespace FinanceTracker.BLL.Interface;

public interface IRoleService
{
     Task<List<RoleModel>> GetRoles();
}
