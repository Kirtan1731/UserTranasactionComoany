using FinanceTracker.DAL;

namespace FinanceTracker.BLL.Interface;

public interface IStatusServices
{
    Task<List<StatusModel>> GetStatuses();
}
