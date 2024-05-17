using FinanceTracker.BLL;
using FinanceTracker.DAL.Dtos;
using FinanceTracker.DAL.DTOs;
using FinanceTracker.DAL.Resources;
using FinanceTracker.Hangfire.Interface;
using FinanceTracker.Model.AuthoritySetting;
using Hangfire;
namespace FinanceTracker.DAL;

public class UserServices : IUserServices
{
    private readonly IUserRepository _userRepository;
    private readonly IMailService _mailService;
    private readonly IAuthorityRegistrationServices _authorityRegistration;
    private readonly AuthorityModel _authoritySettings;
    public UserServices(IUserRepository userRepository, IMailService mailService, IAuthorityRegistrationServices authorityRegistration, AuthorityModel authoritySettings)
    {
        _userRepository = userRepository;
        _authorityRegistration = authorityRegistration;
        _mailService = mailService;
        _authoritySettings = authoritySettings;
    }

    public Task<bool> IsEmailTakenCheck(string email)
    {
        return _userRepository.IsEmailTaken(email);
    }
    public async Task<RoleResponseDto> RegisterUserWithAuthority(UserRequestDto userRequest)
    {
        var userRegistration = new AuthorityRegistrationDto()
        {
            FullName = userRequest.FirstName + userRequest.LastName,
            Email = userRequest.Email,
            UserName = userRequest.Email,
            PasswordHash = userRequest.PasswordHash,
            UserClient = _authoritySettings.ClientId,
            Claims = new List<ClaimsDto>(),
            Roles = new List<RoleDto>()
            {
                new RoleDto { Name= "Employee"}
            }
        };

        var response = await _authorityRegistration.RegisterUser(userRegistration);

        if (response.StatusCode == 200)
        {
            var responseData = new RoleResponseDto();
        try
        {
            if (await IsEmailTakenCheck(userRequest.Email))
            {
                responseData.Error = ValidationResources.EmailAlreadyExist ;
            }
            else
            {
                userRequest.SubjectId = response.Data.SubjectId;
                var data = await _userRepository.AddUser(userRequest);
                BackgroundJob.Enqueue(() => _mailService.SendEmailAdmin(userRequest.FirstName));
                response.Message = ValidationResources.RegistrationSuccess;
            }
        }
        catch (System.Exception ex)
        {

            response.Message = ValidationResources.InternalServer;
            response.Data = null;
            responseData.Error = $"{ex.Message} {ex.InnerException}" ;
        }
        return new RoleResponseDto
        {
            Message = ValidationResources.AuthoritySuccess
        };
        }
        return new RoleResponseDto
        {
            Message = ValidationResources.AuthoritySuccess
        };
    }
}