namespace FinanceTracker.Hangfire.Model
{
    public class ApprovalEmail
    {
        public string Name { get; set; }
        public string ToEmail { get; set; }
        public string RejectionReason { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public ApprovalEmail(string LoginUrl)
        {
            Subject = "Your account creation request has been approved";
            Body = $"<h4>Welcome to PFT</h4><br><p>Hello,<p>Your Request for account creation has been approved, Now you can easily track and manage your finance with our PFT.</p><p>Thank you for choosing us. Please click on the below link to continue.</p><br><p>{LoginUrl}</p>Best Regards,<br>PFT Team.";
        }
    }
}