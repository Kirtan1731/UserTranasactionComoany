using FinanceTracker.BLL;
using FinanceTracker.DAL.Interface.TokenRepository;
using FinanceTracker.DAL.Model.AuthoritySetting;
using FinanceTracker.DAL.Resources;
using FinanceTracker.Model.AuthoritySetting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Bcpg;

namespace FinanceTracker.DAL
{
    public class AuthorityRegistrationServices : IAuthorityRegistrationServices
    {
        private readonly IRestServiceClientRepository _restServiceClient;

        private readonly ITokenRepository _tokenRepository;

        private readonly AuthorityModel _authoritySettings;

        private readonly ILogger<AuthorityRegistrationServices> _logger;

        public AuthorityRegistrationServices(ITokenRepository tokenRepository, IRestServiceClientRepository restServiceClient, AuthorityModel authoritySettings, ILogger<AuthorityRegistrationServices> logger)
        {
            _tokenRepository = tokenRepository;
            _restServiceClient = restServiceClient;
            _authoritySettings = authoritySettings;
            _logger = logger;
        }

        public virtual async Task<ApiResponse<UserResponseModel>> RegisterUser(AuthorityRegistrationDto authorityRegisterModel)
        {
            string baseUri = $"{_authoritySettings.BaseUri}/api/userregistration";

            var token = await _tokenRepository.GenerateTokenAsync();
            if (token == null)
            {
                var Response2 = new ApiResponse<UserResponseModel>()
                {
                    
                    StatusCode = 400,
                    Error = new List<string> {ValidationResources.TokenGenerateError}
                };
                return Response2;
            }

            var response = await _restServiceClient.InvokePostAsync<AuthorityRegistrationDto, ApiResponse<UserResponseModel>>(baseUri, authorityRegisterModel, token);
            string userId = "";
            if (response.IsSuccessful)
            {
                string subjectId = response.SubjectId;
                dynamic data = JsonConvert.DeserializeObject(subjectId);
                bool isActive = false;
                userId = data["subjectId"];
                await UpdateUserStatus(userId, isActive, token);
                var Response = new ApiResponse<UserResponseModel>()
                {
                    Data = new UserResponseModel()
                    {
                        SubjectId = userId,
                        StatusCode = response.StatusCode,
                        IsSuccessful = response.IsSuccessful,
                        ResponseStatus = response.ResponseStatus
                    },
                    StatusCode = response.StatusCode,
                    Error = response.ErrorMessage
                };
                return Response;
            }else{
                var Response = new ApiResponse<UserResponseModel>()
                {
                    StatusCode = response.StatusCode,
                    Error = response.ErrorMessage
                };
                return Response;
            }
        }
        public async Task<ApiResponse<UserResponseModel>> UpdateApprove(string subjectId)
        {
            var tokens = await _tokenRepository.GenerateTokenAsync();
            var uri = $"{_authoritySettings.BaseUri}/api/enable/{subjectId}";
            bool isActive = true;
            var response = await _restServiceClient.InvokePutAsync<string, ApiResponse<UserResponseModel>>(uri, subjectId, isActive, tokens);
            return new ApiResponse<UserResponseModel>
            {

            };
        }
        private async Task<ApiResponse<UserResponseModel>> UpdateUserStatus(string subjectId, bool isActive, string token)
        {
            var uri = $"{_authoritySettings.BaseUri}/api/enable/{subjectId}";
            var response = await _restServiceClient.InvokePutAsync<string, ApiResponse<UserResponseModel>>(uri, subjectId, isActive, token);
            return new ApiResponse<UserResponseModel>
            {

            };
        }
    }
}