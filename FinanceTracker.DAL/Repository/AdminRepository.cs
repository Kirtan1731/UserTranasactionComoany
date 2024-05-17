using FinanceTracker.DAL.Data;
using FinanceTracker.DAL.Dtos;
using FinanceTracker.DAL.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.DAL.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _UserContext;
        public AdminRepository(ApplicationDbContext userContext)
        {
            _UserContext = userContext;
        }
        public async Task<List<PendingUsersDto>> GetPendingUsersList(byte userStatusId, PaginationParameters paginationParams)
        {
            var data = await _UserContext.UserData
                .Where(x => x.UserStatusId == 1)
                .OrderByDescending(x=>x.UserId)
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .Select(x => new PendingUsersDto
                {
                    UserId = x.UserId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmailId = x.EmailId,
                    PhoneNumber = x.PhoneNumber
                })
                .ToListAsync();

            return data;
        }
        public async Task<List<ApprovedUsersDto>> GetApprovedUsersList(byte userStatusId, PaginationParameters paginationParams)
        {
            var data = await _UserContext.UserData
                .Where(x => x.UserStatusId == 2)
                .OrderByDescending(x=>x.UserId)
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .Select(x => new ApprovedUsersDto
                {
                    UserId = x.UserId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmailId = x.EmailId,
                    PhoneNumber = x.PhoneNumber,
                    isActive = x.IsActive
                })
                .ToListAsync();

            return data;
        }
        public async Task<List<RejectedUsersDto>> GetRejectedUsersList(byte userStatusId, PaginationParameters paginationParams)
        {
            var data = await _UserContext.UserData
                .Where(x => x.UserStatusId == 3)
                .OrderByDescending(x=>x.ModifiedDate)
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .Select(x => new RejectedUsersDto
                {
                    UserId = x.UserId,
                    ModifiedDate = x.ModifiedDate,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmailId = x.EmailId,
                    PhoneNumber = x.PhoneNumber,
                    RejectionReason = x.RejectionReason
                })
                .ToListAsync();

            return data;
        }
        public async Task<int> GetTotalUsersCount(byte userStatusId)
        {
            return await _UserContext.UserData
                .Where(x => x.UserStatusId == userStatusId)
                .CountAsync();
        }
        public async Task<UserModel> GetUserById(int userId)
        {
            var user = await _UserContext.UserData.FirstOrDefaultAsync(x => x.UserId == userId);
            return user;
        }

        public async Task UpdateUserStatus(UserModel userModel)
        {
            _UserContext.Update(userModel);
            await _UserContext.SaveChangesAsync();
        }
        public async Task<int?> FindUserIdByRolId(int rolId)
        {
            var user=await _UserContext.UserData.FirstOrDefaultAsync(x=>x.RoleId == rolId);
            return user?.UserId;
        }
    }
}