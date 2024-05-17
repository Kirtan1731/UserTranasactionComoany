using FinanceTracker.DAL.Interface.TokenRepository;
using FinanceTracker.Model.AuthoritySetting;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;

namespace FinanceTracker.DAL.Repository.TokenRepository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly AuthorityModel _authoritySettings;

        private readonly ILogger<TokenRepository> _logger;

        public TokenRepository(AuthorityModel authoritySettings, ILogger<TokenRepository> logger)
        {
            _authoritySettings = authoritySettings;
            _logger = logger;
        }

        public async Task<string> GenerateTokenAsync()
        {
            HttpClient client = new HttpClient();
            string tokenUrl = _authoritySettings.TokenUrl;
            string clientId = _authoritySettings.ClientId;
            string secret = _authoritySettings.Secret;
            var tokenClientOptions = new TokenClientOptions
            {
                Address = tokenUrl,
                ClientId = clientId,
                ClientSecret = secret
            };

            var tokenClient = new TokenClient(client, tokenClientOptions);
            try
            {
                var tokenResponse = await tokenClient.RequestClientCredentialsTokenAsync("1AuthorityApi");
                string token = string.Format("Bearer {0}", tokenResponse?.AccessToken);
                if (tokenResponse.IsError)
                {
                    _logger.LogError("Error while generate token{0}", tokenResponse.Error);
                    return token=null;
                }
                else
                {
                    _logger.LogInformation("Token generated Successfully");
                    return token;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Token Generation failed");
                throw ex;
            }
        }
    }
}
