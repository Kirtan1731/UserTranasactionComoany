
using FinanceTracker.DAL.Data;
using FinanceTracker.DAL.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.DAL.Repository;

public class StatusRepository : IStatusRepository
{
    private readonly ApplicationDbContext _applicationContext;
    public StatusRepository(ApplicationDbContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task<List<StatusModel>> GetStatus()
    {
        return await _applicationContext.UserStatus.ToListAsync();

    }
}
