namespace FinanceTracker.DAL.Interface.TokenRepository
{
    public interface ITokenRepository
    {
        Task<string> GenerateTokenAsync();
    }
}