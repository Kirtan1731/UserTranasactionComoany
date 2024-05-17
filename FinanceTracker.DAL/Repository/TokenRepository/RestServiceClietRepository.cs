using FinanceTracker.DAL.Model.AuthoritySetting;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace FinanceTracker.DAL
{
    public class RestServiceClientRepository : IRestServiceClientRepository
    {

        private readonly ILogger<RestServiceClientRepository> _logger;

        public RestServiceClientRepository(ILogger<RestServiceClientRepository> logger)
        {
            _logger = logger;
        }
        public async Task<UserResponseModel> InvokePostAsync<T, R>(string requestUri, T model, string? token = null) where R : new() where T : class
        {
            var client = new RestSharp.RestClient(requestUri);

            var restRequest = new RestSharp.RestRequest(requestUri, Method.Post)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; }
            };

            restRequest.AddJsonBody(model);
            if (!string.IsNullOrWhiteSpace(token))
            {
                restRequest.AddParameter("Authorization", token, ParameterType.HttpHeader);
            }

            RestResponse<UserResponseModel> response = client.Execute<UserResponseModel>(restRequest);

            if (response.IsSuccessful)
            {
                _logger.LogInformation("POST request to {requestUri} succeeded with response code {statusCode} and response content {responseContent}",
                                       requestUri, response.StatusCode, response?.Content);
                
                return new UserResponseModel
                {
                    StatusCode = 200,
                    IsSuccessful = response.IsSuccessful,
                    ResponseStatus = "Success",
                    SubjectId = response.Content   
                };
            }
            else
            {
                _logger.LogInformation("POST request to {requestUri} failed with response code {statusCode}. Error {Content}",
                                       requestUri, response.StatusCode, response.Content);
                var error = new List<string> { "User Registration Failed" };
                return new UserResponseModel
                {
                    StatusCode = (int)response.StatusCode,
                    ErrorMessage = new List<string> {response.ErrorMessage}
                };
            }
        }
        public async Task<UserResponseModel> InvokePutAsync<T, R>(string requestUri, string subjectId,bool isActive, string? token = null) where R : new() where T : class
        {
            var client = new RestSharp.RestClient(requestUri);

            var restRequest = new RestSharp.RestRequest(requestUri, Method.Put)
            {
                OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; }
            };
            restRequest.AddUrlSegment("subjectId",subjectId);
            restRequest.AddQueryParameter("isEnable",isActive);
            if (!string.IsNullOrWhiteSpace(token))
            {
                restRequest.AddParameter("Authorization", token, ParameterType.HttpHeader);
            }

            RestResponse<UserResponseModel> response = client.Execute<UserResponseModel>(restRequest);
            System.Console.WriteLine(
                "User Updated Successfully"
            );
            return new UserResponseModel{};
        }

    }
}
