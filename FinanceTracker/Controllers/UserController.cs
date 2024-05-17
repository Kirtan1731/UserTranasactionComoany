using FinanceTracker.DAL;
using FinanceTracker.DAL.Dtos;
using FinanceTracker.DAL.DTOs;
using FinanceTracker.DAL.Resources;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly ILogger<UserController> _logger; 

        public UserController(IUserServices userServices, ILogger<UserController> logger) 
        {
            _userServices = userServices;
            _logger = logger;
        }

        [Route("user/register")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserRequestDto userRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorMessage = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    _logger.LogWarning($"{ResponseResources.InvalidModelState} {errorMessage}");
                    var response = new RoleResponseDto
                    {
                        Error = errorMessage,
                    };
                    return BadRequest(response);
                }

                if (await _userServices.IsEmailTakenCheck(userRequestDto.Email))
                {
                    ModelState.AddModelError(ValidationResources.EmailId, ValidationResources.EmailExistMessage);
                    var errorMessage = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    _logger.LogWarning($"{ValidationResources.EmailAlreadyExist} {userRequestDto.Email}");
                    var response = new RoleResponseDto
                    {
                        Error = errorMessage,
                    };
                    return BadRequest(response);
                }

                var newUser = await _userServices.RegisterUserWithAuthority(userRequestDto);
                _logger.LogInformation(ValidationResources.RegistrationSuccess);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ResponseResources.ErrorRegister);
                return BadRequest(ResponseResources.ErrorRegister);
            }
        }
    }
}
