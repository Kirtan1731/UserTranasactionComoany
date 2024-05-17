namespace FinanceTracker.Model.AuthoritySetting
{
    public class AuthorityModel
    {
        public string? TokenUrl { get; set; }

        public string ClientId { get; set; }

        public string? Secret { get; set; }

        public string? BaseUri { get; set; }
        public string? BasePut { get; set;}
    }
}
