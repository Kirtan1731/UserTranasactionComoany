namespace FinanceTracker.Hangfire.Model
{
    public class RejectionEmail
    {
        public string Name { get; set; }
        public string ToEmail { get; set; }
        public string RejectionReason { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public RejectionEmail(string rejectionReason)
        {
            Subject = "PFT: Request Reject";
            Body = $"<p>Hello,</p><p>Your account creation request with PFT has been rejected by the admin.</p><p>Reason for Rejection: {rejectionReason}</p><br>Best Regards,<p><br>PFT Team.";
        }
    }
}