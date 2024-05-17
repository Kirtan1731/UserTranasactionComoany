using FinanceTracker.BLL.Interface;
using FinanceTracker.BLL.shared;
using FinanceTracker.BLL.shared.Enum;
using FinanceTracker.DAL.Data;
using FinanceTracker.DAL.DTO;
using FinanceTracker.DAL.Dtos;
using FinanceTracker.DAL.DTOs;
using FinanceTracker.DAL.Interface;
using FinanceTracker.DAL.Resources;
using FinanceTracker.Hangfire.Interface;
using FinanceTracker.Hangfire.Model;
using Hangfire;

namespace FinanceTracker.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthorityRegistrationServices _authorityRegistration;
        private readonly IMailService _mailService;
        private readonly ApplicationDbContext _userDbContext;

        public AdminService(IAdminRepository adminRepository, IMailService mailService, IAuthorityRegistrationServices authorityRegistration, ApplicationDbContext userDbContext)
        {
            _adminRepository = adminRepository;
            _authorityRegistration = authorityRegistration;
            _mailService = mailService;
            _userDbContext = userDbContext;
        }

        public async Task<ResponseDto> GetPendingUsers(byte userStatusId, PaginationParameters paginationParams)
        {
            var response = new ResponseDto();
            try
            {
                var data = await _adminRepository.GetPendingUsersList(userStatusId, paginationParams);
                var totalCount = await _adminRepository.GetTotalUsersCount(userStatusId);
                response.Data = data.Count > 0
               ? (data.Count < paginationParams.PageSize ? data : data.Take(paginationParams.PageSize).ToList())
               : new List<PendingUsersDto>();
                response.TotalRecordCount = totalCount;
            }
            catch (Exception Ex)
            {
                response.Error = Ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto> GetApprovedUsers(byte userStatusId, PaginationParameters paginationParams)
        {
            var response = new ResponseDto();
            try
            {
                var data = await _adminRepository.GetApprovedUsersList(userStatusId, paginationParams);
                var totalCount = await _adminRepository.GetTotalUsersCount(userStatusId);
                response.Data = data.Count > 0
                ? (data.Count < paginationParams.PageSize ? data : data.Take(paginationParams.PageSize).ToList())
                : new List<ApprovedUsersDto>();
                response.TotalRecordCount = totalCount;
            }
            catch (Exception Ex)
            {
                response.Error = Ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto> GetRejectedUsers(byte userStatusId, PaginationParameters paginationParams)
        {
            var response = new ResponseDto();
            try
            {
                var data = await _adminRepository.GetRejectedUsersList(userStatusId, paginationParams);
                var totalCount = await _adminRepository.GetTotalUsersCount(userStatusId);
                response.Data = data.Count > 0
               ? (data.Count < paginationParams.PageSize ? data : data.Take(paginationParams.PageSize).ToList())
               : new List<RejectedUsersDto>();
                response.TotalRecordCount = totalCount;
            }
            catch (Exception Ex)
            {
                response.Error = Ex.Message;
            }
            return response;
        }

        public async Task<ApprovalResponseDto> UpdateUserStatusId(List<ApprovalRequestDto> userStatusUpdates)
        {
            ApprovalResponseDto response = new ApprovalResponseDto();
            var approvedUsers = new List<int>();
            var rejectedUsers = new List<int>();
            try
            {
                foreach (var userStatus in userStatusUpdates)
                {
                    if (userStatus.UserId == 0)
                    {
                        response.Error = ValidationResources.InvalidUser;
                    }
                    var user = await _adminRepository.GetUserById(userStatus.UserId);
                    if (user == null)
                    {
                        throw new Exception(ValidationResources.InvalidUser);
                    }
                    switch (userStatus.UserStatusId)
                    {
                        case (int)UserStatusEnum.Approved:
                            if (user.UserStatusId != (int)UserStatusEnum.Rejected && user.UserStatusId != (int)UserStatusEnum.Approved)
                            {
                                var adminId = await _adminRepository.FindUserIdByRolId((int)RolesEnum.Admin);
                                user.UserStatusId = (int)UserStatusEnum.Approved;
                                user.ModifiedDate = DateTime.Now;
                                user.IsActive = true;
                                user.RejectionReason = null;
                                user.ModifiedBy = (int)adminId;
                                user.CreatedBy = userStatus.UserId;

                                approvedUsers.Add(userStatus.UserId);
                                await _authorityRegistration.UpdateApprove(user.SubjectId);
                                await _adminRepository.UpdateUserStatus(user);
                                BackgroundJob.Enqueue(() => _mailService.SendApprovalMail(userStatus.UserId, userStatus.EmailId, userStatus.RejectionReason, userStatus.UserStatusId));
                            }
                            break;
                        case (int)UserStatusEnum.Rejected:
                            if (user.UserStatusId != (int)UserStatusEnum.Rejected && user.UserStatusId != (int)UserStatusEnum.Approved)
                            {
                                var adminId = await _adminRepository.FindUserIdByRolId((int)RolesEnum.Admin);
                                user.UserStatusId = (int)UserStatusEnum.Rejected;
                                user.ModifiedDate = DateTime.Now;
                                user.IsActive = false;
                                user.RejectionReason = userStatus.RejectionReason;
                                user.ModifiedBy = (int)adminId;
                                user.CreatedBy = userStatus.UserId;

                                rejectedUsers.Add(userStatus.UserId);
                                await _authorityRegistration.UpdateApprove(user.SubjectId);
                                await _adminRepository.UpdateUserStatus(user);
                                RejectionEmail rejectEmail = new RejectionEmail(userStatus.RejectionReason)
                                {
                                    RejectionReason = _userDbContext.UserData.FirstOrDefault(x => x.UserId == userStatus.UserId).RejectionReason
                                };
                                BackgroundJob.Enqueue(() => _mailService.SendRejectMail(userStatus.UserId, userStatus.EmailId, userStatus.RejectionReason, userStatus.UserStatusId, rejectEmail));
                            }
                            break;
                        default:
                            return new ApprovalResponseDto { Error = ValidationResources.InvalidStatus };
                    }
                }
                if (approvedUsers.Count > 0)
                {
                    response.Message += $"{(approvedUsers.Count == 1 ? ValidationResources.UserIs : ValidationResources.SelectedUser)}{ValidationResources.Approved}";
                }
                else if (rejectedUsers.Count > 0)
                {
                    response.Message += $"{(rejectedUsers.Count == 1 ? ValidationResources.UserIs : ValidationResources.SelectedUser)}{ValidationResources.Rejected}";
                }
                else
                {
                    response.Message += ValidationResources.AlreadyUpdateStatus;
                }
            }
            catch (Exception ex)
            {
                response.Error = ValidationResources.ServerError + ex.Message;
            }
            return response;
        }
    }
}