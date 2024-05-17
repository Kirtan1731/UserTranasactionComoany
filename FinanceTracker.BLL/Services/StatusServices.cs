using FinanceTracker.BLL.Interface;
using FinanceTracker.DAL;
using FinanceTracker.DAL.Interface;

namespace FinanceTracker.BLL.Services;

public class StatusServices : IStatusServices
{
    private readonly IStatusRepository _statusRepository;
    public StatusServices(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }


    public async Task<List<StatusModel>> GetStatuses()
    {
        
      return  await _statusRepository.GetStatus();
    }
}
