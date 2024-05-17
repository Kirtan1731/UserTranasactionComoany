using FinanceTracker.DAL.DTO;
using FinanceTracker.DAL.Dtos;
using FinanceTracker.DAL.DTOs;

namespace FinanceTracker.BLL.Interface
{
    public interface IAdminService
    {
        Task<ResponseDto> GetPendingUsers(byte userStatusId, PaginationParameters paginationParams);
        Task<ResponseDto> GetApprovedUsers(byte userStatusId, PaginationParameters paginationParams);
        Task<ResponseDto> GetRejectedUsers(byte userStatusId, PaginationParameters paginationParams);
        Task<ApprovalResponseDto> UpdateUserStatusId(List<ApprovalRequestDto> userStatusUpdates);
    }
}