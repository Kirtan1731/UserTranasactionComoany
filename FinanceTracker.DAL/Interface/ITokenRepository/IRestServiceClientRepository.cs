using FinanceTracker.DAL.Model.AuthoritySetting;

namespace FinanceTracker.DAL
{
    public interface IRestServiceClientRepository
    {
        Task<UserResponseModel> InvokePostAsync<T, R>(string requestUri, T model, string token = null) where R : new() where T : class;
        Task<UserResponseModel> InvokePutAsync<T, R>(string requestUri, string subjectId,bool isActive, string? token = null) where R : new() where T : class;
    }

}