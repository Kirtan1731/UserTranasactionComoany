namespace FinanceTracker.Hangfire.Model
{
    public class AdminEmail
    {
        public string Name = "Admin";
        public string ToEmail = "pooja.parmar@1rivet.com";
        public string Subject = "New Request From ";
        public string Body { get; set; }
        public AdminEmail(string LoginUrl)
        {
            Body = $"<h4>Hello Admin</h4><p>You have pending requests for approval kindly visit the below link to approve or reject the requests.</p><br>{LoginUrl}<br/>Best regards,<br/>PFT Team";
        }
    }
}