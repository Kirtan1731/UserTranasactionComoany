using FinanceTracker.Hangfire.Model;

namespace FinanceTracker.Hangfire.Interface
{
    public interface IMailService
    {
        Task SendEmailAdmin(string userName);
        Task SendApprovalMail(int id, string userEmail, string RejectionReason, byte statusId);
        Task SendRejectMail(int id, string userEmail, string RejectionReason, byte statusId, RejectionEmail rejectEmail);     
    }
}