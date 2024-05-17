using FinanceTracker.DAL.Model;

namespace FinanceTracker.DAL.Interface;

public interface IStatusRepository
{
    Task<List<StatusModel>> GetStatus();
}
