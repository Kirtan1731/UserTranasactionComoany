using FinanceTracker.DAL.Dtos;

namespace FinanceTracker.DAL.Interface
{
    public interface IAdminRepository
    {
        public Task<List<PendingUsersDto>> GetPendingUsersList(byte userStatusId, PaginationParameters paginationParams);
        public Task<List<ApprovedUsersDto>> GetApprovedUsersList(byte userStatusId, PaginationParameters paginationParams);
        public Task<List<RejectedUsersDto>> GetRejectedUsersList(byte userStatusId, PaginationParameters paginationParams);
        public Task<int> GetTotalUsersCount(byte userStatusId);
        public Task<UserModel> GetUserById(int userId);
        Task UpdateUserStatus(UserModel userModel);
        public Task<int?> FindUserIdByRolId(int rolId);
    }
}