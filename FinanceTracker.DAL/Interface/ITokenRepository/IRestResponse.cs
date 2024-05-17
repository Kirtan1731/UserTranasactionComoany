namespace FinanceTracker.DAL.Interface.ITokenRepository
{
    public interface IUserResponse
{
    int StatusCode { get; set; }
    string? Responses { get; set; }
    List<string>? Error { get; set; }
    string? Token { get; set; }
    string? Username { get; set; }
    bool IsSuccessful { get; set; }
    string? Content { get; set; }
}

}