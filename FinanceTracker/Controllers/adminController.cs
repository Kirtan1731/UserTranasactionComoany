using FinanceTracker.BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using FinanceTracker.DAL.Dtos;
using FinanceTracker.DAL.DTO;
using FinanceTracker.DAL.Resources;
using FinanceTracker.DAL.DTOs;

namespace FinanceTracker.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminServices;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IAdminService adminServices, ILogger<AdminController> logger)
        {
            _logger = logger;
            _adminServices = adminServices;
        }
        [Route("admin/users/{statusId}")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetUsersList(byte statusId, [FromQuery] PaginationParameters paginationParams)
        {
            try
            {
                switch (statusId)
                {
                    case 1:
                        var pendingUsers = await _adminServices.GetPendingUsers(statusId, paginationParams);
                        return pendingUsers != null ? Ok(pendingUsers) : StatusCode(204, Resources.NoContent);
                    case 2:
                        var acceptedUsers = await _adminServices.GetApprovedUsers(statusId, paginationParams);
                        return acceptedUsers != null ? Ok(acceptedUsers) : StatusCode(204, Resources.NoContent);
                    case 3:
                        var rejectedUsers = await _adminServices.GetRejectedUsers(statusId, paginationParams);
                        return rejectedUsers != null ? Ok(rejectedUsers) : StatusCode(204, Resources.NoContent);
                    default:
                        _logger.LogWarning($"{ResponseResources.InvalidStatus} {statusId}");
                        return BadRequest(Resources.Invalid);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ResponseResources.UserError);
                return StatusCode(500, ValidationResources.ServerError);
            }
        }

        [HttpPost("admin/registration-requests")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUsersStatus(List<ApprovalRequestDto> userStatus)
        {
            try
            {
                var result = await _adminServices.UpdateUserStatusId(userStatus);
                if (result.Message != null && result.Error != null)
                {
                    _logger.LogInformation(ResponseResources.UserUpdateSuccess);
                    return StatusCode(200, new ApprovalResponseDto { Message = result.Message, Error = result.Error });

                }else if(result.Message != null){
                     _logger.LogInformation(ResponseResources.UserUpdateSuccess);
                    return StatusCode(200, new ApprovalResponseDto { Message = result.Message});
                }
                _logger.LogWarning($"{ResponseResources.UserUpdateFailed} {result.Error}");
                return StatusCode(400, new ApprovalResponseDto { Error = result.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ResponseResources.UserUpdateError);
                return StatusCode(500, ValidationResources.ServerError);
            }
        }
    }
}